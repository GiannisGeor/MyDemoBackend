using Common.Configuration;
using Common.Methods;
using Data.Interfaces;
using FluentValidation.Results;
using Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using Models.Entities.Auth;
using Serilog;
using Services.Dtos;
using Services.Dtos.Auth;
using Services.Interfaces;
using Services.Interfaces.Auth;
using Services.ResponseDtos.Auth;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace Services.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UrlEncoder _urlEncoder;
        private readonly IEmailService _emailService;
        private readonly ICustomerRepository _customerRepository;

        public AuthenticationService(UserManager<AuthUser> userManager,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            UrlEncoder urlEncoder,
            ICustomerRepository customerRepository,
            IEmailService emailService)
        {
            _userManager = userManager;
            _customerRepository = customerRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _urlEncoder = urlEncoder;
            _emailService = emailService;

        }


        public async Task<ValueResponse<bool>> RegisterNewUser(RegisterNewUserDto newUser)
        {

            ValueResponse<bool> response = new();

            try
            {
                // check if user already exists
                var userExists = await _userManager.FindByEmailAsync(newUser.Email);
                if (userExists != null)
                {
                    ValidationResult validationResult = new ValidationResult();
                    validationResult.Errors.Add(new ValidationFailure("RegisterNewUser", $"User {newUser.Email} already registered"));
                    response.SetFailureWithValidation(validationResult);
                    return response;
                }

                // Create a new AuthUser instance
                var authUser = new AuthUser()
                {
                    UserName = newUser.Email,
                    Email = newUser.Email,                    
                    PhoneNumber = newUser.Phone,
                    EmailConfirmed = false
                };       

                // Saves the new user and Extract errors in case of failure
                var registrationProcess = await _userManager.CreateAsync(authUser, newUser.Password);
                if (!registrationProcess.Succeeded)
                {
                    ValidationResult extractedErrors = ExtractErrorsAndAddInValidationResult(registrationProcess);
                    response.SetFailureWithValidation(extractedErrors);
                    return response;
                }

                var customer = new Customer()
                {
                    Name = newUser.Name,
                    Phone = newUser.Phone,
                    Email = newUser.Email,
                };
                customer.MarkNew();

                Customer entity = await _customerRepository.AddNewCustomer(customer);

                // Check Email Confirmation configuration setting
                if (CheckIfRequireConfirmedEmailIsEnabled())
                {
                    // Generate and send confirmation Token
                    var sendEmail = await GenerateAndSendConfirmationToken(authUser);
                }

                response.SetSuccess(true);
                return response;
            }
            catch (Exception)
            {

                response.SetHttpFailureCode($"Failed register user", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ObjectResponse<string>> LoginForms(LoginDto loginDto)
        {
            // Prepare response
            ObjectResponse<string> response = new ObjectResponse<string>();

            try
            {

                if (loginDto.Email == null || loginDto.Password == null)
                {
                    response.SetHttpFailureCode("Invalid login.", HttpResultCode.Unauthorized);
                    return response;
                }

                // Try to Load user data
                AuthUser authUser = await _userManager.FindByEmailAsync(loginDto.Email);
                if (authUser == null)
                {
                    response.SetHttpFailureCode("Invalid login.", HttpResultCode.Unauthorized);
                    return response;
                }

                // If locked out, prevent login
                bool lockoutEnabled = int.Parse(_configuration["Auth:Identity:Lockout:MaxFailedAccessAttempts"]) > 0;
                if (lockoutEnabled)
                {
                    if (authUser.LockoutEnd > DateTime.UtcNow)
                    {
                        response.SetHttpFailureCode("Locked out.", HttpResultCode.Unauthorized);
                        return response;
                    }
                }

                // User exists, try to log in with password
                if (await _userManager.CheckPasswordAsync(authUser, loginDto.Password))
                {
                    // Check Email Confirmation configuration setting
                    if (CheckIfRequireConfirmedEmailIsEnabled())
                    {
                        if (!authUser.EmailConfirmed)
                        {
                            // Construct validation result with custom errorcode to communicate the issue with front-end.
                            ValidationResult validationResult = new ValidationResult();
                            var validationError = new ValidationFailure("Login", "You need to confirm your Email.");
                            validationError.ErrorCode = GlobalConstants.CustomErrorCodes.EmailNotConfirmed;
                            validationResult.Errors.Add(validationError);

                            // Send response with translated validation error.
                            response.SetFailureWithValidation(validationResult);
                            return response;
                        }
                    }

                    // Reset lockout count
                    if (lockoutEnabled)
                    {
                        await _userManager.ResetAccessFailedCountAsync(authUser);
                    }

                    Customer customer = await _customerRepository.GetCustomerByEmail(loginDto.Email);
                    if (customer == null)
                    {
                        response.SetHttpFailureCode("Username does not exist.", HttpResultCode.BadRequest);
                        return response;
                    }

                    // Select TTL
                    DateTime expires = loginDto.RememberMe
                        ? DateTime.UtcNow.AddDays(int.Parse(_configuration["Auth:RefreshToken:LongTTL"]))
                        : DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Auth:RefreshToken:ShortTTL"]));

                    // Builds the claims, the refresh token and return the access token
                    var accessToken = BuildTokens(authUser.Id,
                        authUser.UserName,
                        authUser.Email,
                        authUser.TwoFactorEnabled,
                        false,
                        GlobalConstants.Authentication.Roles.Customer,
                        expires,
                        customer.Id);

                    response.SetSuccess(accessToken);
                    return response;
                }

                // Handle lockout
                if (lockoutEnabled)
                {
                    await _userManager.AccessFailedAsync(authUser);
                }

                response.SetHttpFailureCode("Invalid login.", HttpResultCode.Unauthorized);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, "Error in AuthenticationService.LoginForms.");
                response.SetHttpFailureCode("Error while logging in.", HttpResultCode.InternalServerError);
                return response;
            }

        }

        public async Task<ObjectResponse<string>> GenerateAccessToken(string refreshToken)
        {
            // Prepare response
            ObjectResponse<string> response = new ObjectResponse<string>();

            try
            {
                // Parse refresh token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Auth:RefreshToken:SigningKey"]);
                tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Auth:JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Auth:JWT:ValidAudience"],
                    // Set clockskew to zero so tokens expire exactly at token expiration time
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                // Map token
                var parsedToken = (JwtSecurityToken)validatedToken;

                // Check if token expired
                var expired = DateTime.UtcNow > parsedToken.ValidTo;
                if (expired)
                {
                    response.SetHttpFailureCode("Invalid token.", HttpResultCode.Unauthorized);
                    return response;
                }

                // Create and return access token
                var accessToken = GenerateAccessToken(parsedToken);
                response.SetSuccess(new JwtSecurityTokenHandler().WriteToken(accessToken));
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, "Error in AuthenticationService.GenerateAccessToken.");
                response.SetHttpFailureCode("Failed to generate access token.", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ValueResponse<bool>> ConfirmEmail(Guid userid, string emailToken)
        {
            ValueResponse<bool> response = new ValueResponse<bool>();
            try
            {
                // Find user from Email
                var authUser = await _userManager.FindByIdAsync(userid.ToString());
                if (authUser == null)
                {
                    response.SetHttpFailureCode($"User was not found", HttpResultCode.NotFound);
                    return response;
                }

                // Check if user already confirmed his email
                if (authUser.EmailConfirmed) {
                    response.SetHttpFailureCode($"User has already confirmed his email", HttpResultCode.NotFound);
                    return response;
                }

                // Decode Token and run Confirmation
                var decodedToken = HttpUtility.UrlDecode(emailToken);
                var result = await _userManager.ConfirmEmailAsync(authUser, decodedToken);
                if (!result.Succeeded)
                {
                    response.SetHttpFailureCode($"Token is not correct for user: {authUser.Email}", HttpResultCode.BadRequest);
                    return response;
                }

                // Finalize result
                response.SetSuccess(true);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Failed during user confirmation procedure for userid:{userid}");
                response.SetHttpFailureCode($"Failed during user confirmation procedure", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ValueResponse<bool>> ResendConfirmationEmail(string email)
        {

            ValueResponse<bool> response = new ValueResponse<bool>();
            try
            {

                // Validation
                var emailValidationResult = GlobalMethods.ValidateEmail(email);
                if (!emailValidationResult.IsValid)
                {
                    response.SetFailureWithValidation(emailValidationResult);
                    return response;
                }

                // Check if Email Confirmation is required
                if (!CheckIfRequireConfirmedEmailIsEnabled())
                {
                    response.SetHttpFailureCode($"Email Confirmation is not required", HttpResultCode.BadRequest);
                    return response;
                }

                // Get user and check if user exists
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    response.SetHttpFailureCode($"User with email : {email} is not a registered user", HttpResultCode.NotFound);
                    return response;
                }

                // Check if user has already confirmed his email
                if (user.EmailConfirmed)
                {
                    response.SetHttpFailureCode($"User {user.Email} already confirmed his email", HttpResultCode.BadRequest);
                    return response;
                }

                // Generate and send confirmation Token
                var sendEmail = await GenerateAndSendConfirmationToken(user);

                // Finalize response and return
                response.SetSuccess(true);
                return response; ;

            }
            catch (Exception e)
            {
                Log.Error(e, $"Failure during resend confirmation Email");
                response.SetHttpFailureCode($"Failure during resend confirmation Email", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ValueResponse<bool>> ChangePassword(string currentPassword, string newPassword)
        {
            ValueResponse<bool> response = new ValueResponse<bool>();
            try
            {

                // grab email from httpContext
                var userEmail = _httpContextAccessor.HttpContext.Request.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;

                // Find the user
                AuthUser authUser = await _userManager.FindByEmailAsync(userEmail);
                if (authUser == null)
                {
                    response.SetHttpFailureCode($"User was not found", HttpResultCode.NotFound);
                    return response;
                }

                // check password is not the same or null
                if (currentPassword == null || newPassword == null || currentPassword.Equals(newPassword))
                {
                    response.SetHttpFailureCode("Passwords can not be equals or empty", HttpResultCode.BadRequest);
                    return response;
                }

                // change password
                var result = await _userManager.ChangePasswordAsync(authUser, currentPassword, newPassword);
                if (!result.Succeeded)
                {
                    var extractedErrors = ExtractErrorsAndAddInValidationResult(result);

                    response.SetFailureWithValidation(extractedErrors);
                    return response;
                }

                response.SetSuccess(true);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Failed during user procedure ChangePassword");
                response.SetHttpFailureCode($"Failed during user procedure ChangePassword", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ValueResponse<bool>> ForgotPassword(string email)
        {
            ValueResponse<bool> response = new ValueResponse<bool>();
            try
            {
                // Find user by email
                var authUser = await _userManager.FindByEmailAsync(email);
                if (authUser == null)
                {
                    response.SetHttpFailureCode($"User was not found", HttpResultCode.NotFound);
                    return response;
                }

                // Generate token 
                var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(authUser);

                // Encode token
                var encodedResetPasswordToken = HttpUtility.UrlEncode(resetPasswordToken);

                // Send email with redirecting link
                var result = await _emailService.GenericEmailSend(await _emailService.ConstructEmailNotificationRequiredInfoObject(emailType: EmailType.ForgotPassword, authUser: authUser , encodedResetPasswordToken: encodedResetPasswordToken ));
                if (!result.Success)
                {
                    response.SetHttpFailureCode($"Email was not send for user: {authUser.Email}, in order to change password", HttpResultCode.BadRequest);
                    return response;
                }

                response.SetSuccess(true);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Failed during user confirmation procedure for userid:");
                response.SetHttpFailureCode($"Failed during user confirmation procedure", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ValueResponse<bool>> ResetPassword(Guid authUserId, string newPassword, string encodedToken)
        {

            ValueResponse<bool> response = new ValueResponse<bool>();
            try
            {
                // Find user from Email
                var authUser = await _userManager.FindByIdAsync(authUserId.ToString());
                if (authUser == null)
                {
                    response.SetHttpFailureCode($"User was not found", HttpResultCode.NotFound);
                    return response;
                }

                //Decode Token and run Confirmation
                var decodedToken = HttpUtility.UrlDecode(encodedToken);

                // new password
                var result = await _userManager.ResetPasswordAsync(authUser, decodedToken, newPassword);
                if (!result.Succeeded)
                {
                    var extractedErrors = ExtractErrorsAndAddInValidationResult(result);

                    response.SetFailureWithValidation(extractedErrors);
                    return response;
                }

                //Finalize result
                response.SetSuccess(true);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Failed during password reset procedure");
                response.SetHttpFailureCode($"Failed during password reset procedure", HttpResultCode.InternalServerError);
                return response;
            }
        }

        #region Multi Factor Authentication

        // 1) Generates the code for the initial 2FA setup - device
        public async Task<ObjectResponse<QRCodeResponseDto>> Generate2FASetupKey()
        {

            ObjectResponse<QRCodeResponseDto> response = new ObjectResponse<QRCodeResponseDto>();
            QRCodeResponseDto qrCodeResponseDto = new QRCodeResponseDto();
            try
            {
                // grab email from httpContext
                var userEmail = _httpContextAccessor.HttpContext.Request.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;

                // Ensure the user has gone through the username & password screen first
                AuthUser authUser = await _userManager.FindByEmailAsync(userEmail);
                if (authUser == null)
                {
                    response.SetHttpFailureCode("Unable to load Two-Factor Authentication for current user.", HttpResultCode.Unauthorized);
                    return response;
                }

                // Ensure the user has the 2fa disabled
                var is2FAEnabled = await _userManager.GetTwoFactorEnabledAsync(authUser);
                if (is2FAEnabled)
                {
                    response.SetHttpFailureCode("Two-Factor Authentication already enabled.", HttpResultCode.BadRequest);
                    return response;
                }

                // Load the authenticator key and create QRCode uri
                var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(authUser);
                if (string.IsNullOrEmpty(authenticatorKey))
                {
                    await _userManager.ResetAuthenticatorKeyAsync(authUser);
                    authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(authUser);
                }
                var qrCodeUri = GenerateQrCodeUri(authUser.Email, authenticatorKey);
                var readableKey = GenerateReadableKey(authenticatorKey);

                // create QRCode object
                qrCodeResponseDto.Uri = qrCodeUri;
                qrCodeResponseDto.Key = readableKey;

                // return QRCode uri and key
                response.SetSuccess(qrCodeResponseDto);
                return response;

            }
            catch (Exception)
            {
                response.SetHttpFailureCode($"Failed to generate QR code", HttpResultCode.InternalServerError);
                return response;
            }
        }

        // 2) Checks the sent code from the Generate2FASetupKey, and return the new access token with the recovery codes
        public async Task<ObjectResponse<SetUp2FAResponseDto>> Setup2FA(string sixDigits)
        {
            ObjectResponse<SetUp2FAResponseDto> response = new ObjectResponse<SetUp2FAResponseDto>();
            try
            {

                // grab email from httpContext
                var userEmail = _httpContextAccessor.HttpContext.Request.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;

                AuthUser authUser = await _userManager.FindByEmailAsync(userEmail);
                if (authUser == null)
                {
                    response.SetHttpFailureCode($"Unable to find the user - {authUser.Email}.", HttpResultCode.NotFound);
                    return response;
                }

                // verify 2fa is not already enabled
                var is2FAEnabled = await _userManager.GetTwoFactorEnabledAsync(authUser);
                if (is2FAEnabled)
                {
                    response.SetHttpFailureCode($"Two-Factor Authentication is already enabled for the user - {authUser.Email}", HttpResultCode.Unauthorized);
                    return response;
                }

                // validate 2FA token for the user, if true the user is2faAuthenticated
                var validate2FA = await _userManager.VerifyTwoFactorTokenAsync(authUser, TokenOptions.DefaultAuthenticatorProvider, sixDigits);
                if (!validate2FA)
                {
                    response.SetHttpFailureCode($"Verification of Two-Factor Authentication failed for the user - {authUser.Email}", HttpResultCode.Unauthorized);
                    return response;
                }

                // set 2FA to enabled
                var enabled2FA = await _userManager.SetTwoFactorEnabledAsync(authUser, true);
                if (!enabled2FA.Succeeded)
                {
                    response.SetHttpFailureCode($"Activation of Two-Factor Authentication failed for the user - {authUser.Email}", HttpResultCode.Unauthorized);
                    return response;
                }

                // provide 2FA recovery-keys 
                var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(authUser, 1);

                // Get the Refresh Token expiration date from claims
                DateTime refreshTokenExpirationDate = GetDateTimeFromTimestamp(
                    _httpContextAccessor.HttpContext.Request.HttpContext.User
                    .FindFirst(GlobalConstants.Authentication.CustomClaims.RefreshTokenExp).Value);

                // Builds the claims, the refresh token and return the access token
                var accessToken = BuildTokens(authUser.Id,
                    authUser.UserName,
                    authUser.Email,
                    enabled2FA.Succeeded,
                    validate2FA,
                    _httpContextAccessor.HttpContext.Request.HttpContext.User.FindFirst(ClaimTypes.Role).Value,
                    refreshTokenExpirationDate);

                SetUp2FAResponseDto setUp2FAResponseDto = new SetUp2FAResponseDto();
                setUp2FAResponseDto.AccessToken = accessToken;
                setUp2FAResponseDto.RecoveryCodes = recoveryCodes.First();

                response.SetSuccess(setUp2FAResponseDto, HttpResultCode.Ok);
                return response;
            }
            catch (Exception)
            {
                response.SetHttpFailureCode("Error during set up of Two-Factor Authentication", HttpResultCode.InternalServerError);
                return response;

            }
        }

        // 3) Check the sent 2FA code and setup tokens accordingly
        public async Task<ObjectResponse<string>> Authenticate2FA(string sixDigits)
        {
            ObjectResponse<string> response = new ObjectResponse<string>();

            try
            {
                // grab email from httpContext
                var userEmail = _httpContextAccessor.HttpContext.Request.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;

                AuthUser authUser = await _userManager.FindByEmailAsync(userEmail);
                if (authUser == null)
                {
                    response.SetHttpFailureCode($"Unable to find the user - {authUser.Email}.", HttpResultCode.NotFound);
                    return response;
                }

                // Ensure the user has the 2fa enabled
                var is2FAEnabled = await _userManager.GetTwoFactorEnabledAsync(authUser);
                if (!is2FAEnabled)
                {
                    response.SetHttpFailureCode($"Two-Factor Authentication is not enabled for the user - {authUser.Email}.", HttpResultCode.BadRequest);
                    return response;
                }

                // check is not IsTwoFactorAuthenticated
                var is2FAAuthenicated = _httpContextAccessor.HttpContext.Request.HttpContext.
                                            User.Claims.First(x => x.Type == "IsTwoFactorAuthenticated").Value;
                if (is2FAAuthenicated == "True")
                {
                    response.SetHttpFailureCode($"The user - {authUser.Email} is already Two-Factor Authenticated ", HttpResultCode.Unauthorized);
                    return response;
                }

                // verify 2FA
                var verified2FA = await _userManager.VerifyTwoFactorTokenAsync(authUser, TokenOptions.DefaultAuthenticatorProvider, sixDigits);
                if (!verified2FA)
                {
                    response.SetHttpFailureCode($"Verification of Two-Factor Authentication failed for the user - {authUser.Email}", HttpResultCode.Unauthorized);
                    return response;
                }

                //Get the Refresh Token expiration date from claims
                DateTime refreshTokenExpirationDate = GetDateTimeFromTimestamp(
                    _httpContextAccessor.HttpContext.Request.HttpContext.User
                    .FindFirst(GlobalConstants.Authentication.CustomClaims.RefreshTokenExp).Value);

                var newAccessToken = BuildTokens(authUser.Id,
                    authUser.UserName,
                    authUser.Email,
                    is2FAEnabled,
                    true,
                    _httpContextAccessor.HttpContext.Request.HttpContext.User.FindFirst(ClaimTypes.Role).Value,
                    refreshTokenExpirationDate);

                response.SetSuccess(newAccessToken);
                return response;
            }
            catch (Exception)
            {
                response.SetHttpFailureCode("", HttpResultCode.InternalServerError);
                return response;
            }

        }

        // Checks recovery codes, and if okay, remove 2FA from account
        public async Task<ObjectResponse<string>> Reset2FAByRecoveryCode(string recoveryKey)
        {
            ObjectResponse<string> response = new ObjectResponse<string>();

            try
            {
                // grab email from httpContext
                var userEmail = _httpContextAccessor.HttpContext.Request.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                AuthUser authUser = await _userManager.FindByEmailAsync(userEmail);
                if (authUser == null)
                {
                    response.SetHttpFailureCode($"Unable to find the user - {authUser.Email}.", HttpResultCode.NotFound);
                    return response;
                }

                // validates if the recovery codes were found for that user
                var recoveryKeyIsValid = await _userManager.RedeemTwoFactorRecoveryCodeAsync(authUser, recoveryKey);
                if (!recoveryKeyIsValid.Succeeded)
                {
                    ValidationResult validationResult = ExtractErrorsAndAddInValidationResult(recoveryKeyIsValid);

                    response.SetFailureWithValidation(validationResult);
                    return response;
                }

                // reset the AuthKey of the user
                var reset = await _userManager.ResetAuthenticatorKeyAsync(authUser);
                if (!reset.Succeeded)
                {

                    ValidationResult validationResult = ExtractErrorsAndAddInValidationResult(recoveryKeyIsValid);

                    response.SetFailureWithValidation(validationResult);
                    return response;
                }

                // diable 2FA from user account
                var disable2FA = await _userManager.SetTwoFactorEnabledAsync(authUser, false);
                if (!disable2FA.Succeeded)
                {
                    ValidationResult validationResult = ExtractErrorsAndAddInValidationResult(recoveryKeyIsValid);

                    response.SetFailureWithValidation(validationResult);
                    return response;
                }

                //Get the Refresh Token expiration date from claims
                DateTime refreshTokenExpirationDate = GetDateTimeFromTimestamp(
                    _httpContextAccessor.HttpContext.Request.HttpContext.User
                    .FindFirst(GlobalConstants.Authentication.CustomClaims.RefreshTokenExp).Value);

                var accessToken = BuildTokens(authUser.Id,
                    authUser.UserName,
                    authUser.Email,
                    false,
                    false,
                    _httpContextAccessor.HttpContext.Request.HttpContext.User.FindFirst(ClaimTypes.Role).Value,
                    refreshTokenExpirationDate);

                response.SetSuccess(accessToken);
                return response;
            }
            catch (Exception)
            {

                response.SetHttpFailureCode("Error during reset of Two-Factor Authentication", HttpResultCode.InternalServerError);
                return response;
            }

        }

        // Remove the 2FA from a logged in user
        public async Task<ObjectResponse<string>> Remove2FAFromAccount()
        {
            ObjectResponse<string> response = new ObjectResponse<string>();

            try
            {
                // grab email from httpContext
                var userEmail = _httpContextAccessor.HttpContext.Request.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;

                AuthUser authUser = await _userManager.FindByEmailAsync(userEmail);
                if (authUser == null)
                {
                    response.SetHttpFailureCode($"Unable to find the user - {authUser.Email}.", HttpResultCode.NotFound);
                    return response;
                }

                // verify 2fa is enabled
                var is2FAEnabled = await _userManager.GetTwoFactorEnabledAsync(authUser);
                if (!is2FAEnabled)
                {
                    response.SetHttpFailureCode($"Two-Factor Authentication is not enabled for the user - {authUser.Email}", HttpResultCode.Unauthorized);
                    return response;
                }

                // deletes recovery codes so the registered device will be removed
                var resetAuthKey = await _userManager.ResetAuthenticatorKeyAsync(authUser);
                if (!resetAuthKey.Succeeded)
                {
                    ValidationResult validationResult = ExtractErrorsAndAddInValidationResult(resetAuthKey);

                    response.SetFailureWithValidation(validationResult);
                    return response;
                }

                // set 2FA to disabled
                var disable2FA = await _userManager.SetTwoFactorEnabledAsync(authUser, false);
                if (!disable2FA.Succeeded)
                {
                    ValidationResult validationResult = ExtractErrorsAndAddInValidationResult(disable2FA);

                    response.SetFailureWithValidation(validationResult);
                    return response;
                }

                //Get the Refresh Token expiration date from claims
                DateTime refreshTokenExpirationDate = GetDateTimeFromTimestamp(
                    _httpContextAccessor.HttpContext.Request.HttpContext.User
                    .FindFirst(GlobalConstants.Authentication.CustomClaims.RefreshTokenExp).Value);

                var accessToken = BuildTokens(authUser.Id,
                    authUser.UserName,
                    authUser.Email,
                    false,
                    false,
                    _httpContextAccessor.HttpContext.Request.HttpContext.User.FindFirst(ClaimTypes.Role).Value,
                    refreshTokenExpirationDate);

                response.SetSuccess(accessToken);
                return response;
            }
            catch (Exception)
            {

                response.SetHttpFailureCode("Error during removal of Two-Factor Authentication", HttpResultCode.InternalServerError);
                return response;
            }
        }

        #endregion

        #region Private Methods 

        /// <summary>
        /// Builds the claims, the refresh token and return the access token, 
        /// on each occassion accordingly
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="HasTwoFactorEnabled"></param>
        /// <param name="isTwoFactorAuthenticated"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        private string BuildTokens(string userId,
            string userName,
            string userEmail,
            bool HasTwoFactorEnabled,
            bool isTwoFactorAuthenticated,
            string role,
            DateTime expires,
            int? customerId = null)
        {

            //Convert Expiration date to timestamp
            DateTimeOffset dateTimeOffset = new DateTimeOffset(expires);
            var refreshTokeExpirationTimestamp = dateTimeOffset.ToUnixTimeMilliseconds();

            //Get the enforceBackend value from configuration.
            var TwoFactorEnforcedFromBackend = bool.Parse(_configuration["Auth:TwoFactor:EnforceTwoFactor"]);

            // Build claims
            var authClaims = new List<Claim>
                    {
                        new(ClaimTypes.Name, userName),
                        new(ClaimTypes.Upn, userId),
                        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new(GlobalConstants.Authentication.CustomClaims.TwoFactorEnforced, TwoFactorEnforcedFromBackend.ToString()),
                        new(GlobalConstants.Authentication.CustomClaims.HasTwoFactorEnabled, HasTwoFactorEnabled.ToString()),
                        new(GlobalConstants.Authentication.CustomClaims.IsTwoFactorAuthenticated, isTwoFactorAuthenticated.ToString()),
                        new(GlobalConstants.Authentication.CustomClaims.Role, role),
                        new(GlobalConstants.Authentication.CustomClaims.Email, userEmail),
                        new(GlobalConstants.Authentication.CustomClaims.RefreshTokenExp, refreshTokeExpirationTimestamp.ToString()),
                    };

            if (customerId.HasValue)
            {
                Claim customerIdClaim = new Claim(GlobalConstants.Authentication.CustomClaims.CustomerId, customerId.Value.ToString());
                authClaims.Add(customerIdClaim);
            }

            // Generate token
            var refreshToken = GenerateRefreshToken(authClaims, expires);
            AttachNecessaryCookiesToRequest(refreshToken);

            // Generate JWT from refresh token
            JwtSecurityToken accessToken = GenerateAccessToken(refreshToken);

            return (new JwtSecurityTokenHandler().WriteToken(accessToken));
        }

        /// <summary>
        /// Returns true if RequireConfirmedEmail from application configuration is true. Otherwise false
        /// </summary>
        /// <returns></returns>
        private bool CheckIfRequireConfirmedEmailIsEnabled()
        {
            return bool.Parse(_configuration["Auth:Identity:SignIn:RequireConfirmedEmail"]);
        }

        /// Attach Refresh Token Cookie to request
        /// </summary>
        /// <param name="refreshToken"></param>
        private void AttachNecessaryCookiesToRequest(JwtSecurityToken refreshToken)
        {

            // Attach refresh token as http only cookie
            _httpContextAccessor.HttpContext.Response.Cookies
                .Append(GlobalConstants.Authentication.Cookies.RefreshTokenCookie, new JwtSecurityTokenHandler().WriteToken(refreshToken), new CookieOptions()
                {
                    HttpOnly = bool.Parse(_configuration["Auth:Cookies:Refresh:HttpOnly"]),
                    SameSite = (SameSiteMode)int.Parse(_configuration["Auth:Cookies:Refresh:SameSite"]),
                    Secure = bool.Parse(_configuration["Auth:Cookies:Refresh:Secure"]),
                    Expires = GlobalConstants.Authentication.MaxDateTime
                });

        }

        /// <summary>
        /// Generate a new refresh token for given claims
        /// </summary>
        /// <param name="claims">The claims to be included in the refresh token</param>
        /// <param name="keepSignedIn">Sets the lifetime of the refresh token</param>
        /// <returns>A refresh token for the user</returns>
        private JwtSecurityToken GenerateRefreshToken(List<Claim> claims, DateTime expires)
        {
            // Create refresh JWT Token
            var authSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:RefreshToken:SigningKey"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Auth:JWT:ValidIssuer"],
                audience: _configuration["Auth:JWT:ValidAudience"],
                expires: expires,
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        /// <summary>
        /// Generate an access token based on the refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        private JwtSecurityToken GenerateAccessToken(JwtSecurityToken refreshToken)
        {
            // Create access Token
            var authSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:JWT:SigningKey"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Auth:JWT:ValidIssuer"],
                audience: _configuration["Auth:JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Auth:JWT:AccessTokenTTL"])),
                claims: refreshToken.Claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        /// <summary>
        /// Triggerd when the registration of new user fails 
        /// converts the IdentityErrors to Validation results
        /// </summary>
        /// <param name="registration"></param>
        /// <returns> Validation Errors</returns>
        private ValidationResult ExtractErrorsAndAddInValidationResult(IdentityResult registration)
        {
            ValidationResult validationResult = new ValidationResult();

            foreach (var item in registration.Errors)
            {
                validationResult.Errors.Add(new ValidationFailure(item.Code, item.Description));
            }

            return validationResult;
        }

        /// <summary>
        /// Generate Token, Encode it and send confirmation url to user
        /// </summary>
        /// <param name="authUser">User</param>
        /// <param name="order">Order</param>
        /// <returns></returns>
        private async Task<ValueResponse<bool>> GenerateAndSendConfirmationToken(AuthUser authUser)
        {
            var generatedConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(authUser);
            var EncodedConfirmationToken = HttpUtility.UrlEncode(generatedConfirmationToken);

            //return await _emailService.SendConfirmationEmail(authUser, order, EncodedConfirmationToken);
            return await _emailService.GenericEmailSend(await _emailService.ConstructEmailNotificationRequiredInfoObject(emailType: EmailType.ConfirmEmail, authUser: authUser, uri: EncodedConfirmationToken));
        }

        /// <summary>
        /// Generate a key which is better readable for humans
        /// </summary>
        /// <param name="unformattedKey">The unformatted key</param>
        /// <returns>The readable key</returns>
        private string GenerateReadableKey(string unformattedKey)
        {
            // Add a space every 4 characterd
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition, 4)).Append(' ');
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition));
            }
            return result.ToString().ToLowerInvariant();
        }

        /// <summary>
        /// Generate the QR-Code for the authenticator app
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <param name="unformattedKey">The unformatted authenticator key of the user</param>
        /// <returns>A formatted string displayable as a QR-Code</returns>
        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                GlobalConstants.Authentication.TwoFactor.AuthenticatorUriFormat,
                _urlEncoder.Encode("test"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }

        /// <summary>
        /// Return the LocalDateTime based on rtoken Expiration claim timestamp
        /// </summary>
        /// <param name="refreshTokenExpTimeStamp"></param>
        /// <returns></returns>
        private DateTime GetDateTimeFromTimestamp(string refreshTokenExpTimeStamp)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(refreshTokenExpTimeStamp)).LocalDateTime;
        }

        #endregion

    }
}
