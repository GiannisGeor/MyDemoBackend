using Messages;
using Services.Dtos;
using Services.Dtos.Auth;
using Services.ResponseDtos.Auth;

namespace Services.Interfaces.Auth
{
    public interface IAuthenticationService
    {

        /// <summary>
        /// Registers new form users
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="newUser"></param>
        /// <returns> Boolean </returns>
        Task<ValueResponse<bool>> RegisterNewUser(RegisterNewUserDto newUser);

        /// <summary>
        /// Log in with a registered account
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns> Access Token </returns>
        Task<ObjectResponse<string>> LoginForms(LoginDto loginDto);

        /// <summary>
        /// Generates Access Token 
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns> JWT </returns>
        Task<ObjectResponse<string>> GenerateAccessToken(string refreshToken);

        #region Multi Factor Authentication

        /// <summary>
        /// 1) Generates the QR Codes in order to start the 2fa setup procedure
        /// </summary>
        /// <returns> QR Code for the authenticator app</returns>
        Task<ObjectResponse<QRCodeResponseDto>> Generate2FASetupKey();

        /// <summary>
        /// 2) Device Registration using the QR Code produced from step 1
        /// returns the recovery codes
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Recovery Codes</returns>
        Task<ObjectResponse<SetUp2FAResponseDto>> Setup2FA(string token);

        /// <summary>
        /// 3) Authenticates the 2FA for the Logged in user
        /// </summary>
        /// <param name="sixDigits"></param>
        /// <returns></returns>
        Task<ObjectResponse<string>> Authenticate2FA(string sixDigits);

        /// <summary>
        /// Resets the 2FA of the user by the recovery key
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="recoveryKey"></param>
        /// <returns> new access token </returns>
        Task<ObjectResponse<string>> Reset2FAByRecoveryCode(string recoveryKey);

        /// <summary>
        /// Removes the 2FA from the account
        /// </summary>
        /// <returns> new access token</returns>
        Task<ObjectResponse<string>> Remove2FAFromAccount();

        /// <summary>
        /// Comfirms Customer Email
        /// </summary>
        /// <returns></returns>
        Task<ValueResponse<bool>> ConfirmEmail(Guid userId, string token);

        /// <summary>
        /// Resends the confirmation email to the user email provided
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<ValueResponse<bool>> ResendConfirmationEmail(string email);

        /// <summary>
        /// Changes the password of the already logged in user
        /// </summary>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<ValueResponse<bool>> ChangePassword(string currentPassword, string newPassword);

        /// <summary>
        /// A user can reset his password via email
        /// </summary>
        /// <returns></returns>
        Task<ValueResponse<bool>> ForgotPassword(string email);

        /// <summary>
        /// Resets the password of a user, after the forgot password process
        /// </summary>
        /// <param name="authUserId"></param>
        /// <param name="newPassword"></param>
        /// <param name="resetToken"></param>
        /// <returns></returns>
        Task<ValueResponse<bool>> ResetPassword(Guid authUserId, string newPassword, string resetToken);

        #endregion

    }
}
