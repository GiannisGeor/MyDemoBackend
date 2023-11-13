using Common.Configuration;
using Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Auth;
using Services.ResponseDtos.Auth;

namespace Api.Controllers.Auth.Multi_Factor_Authentication
{
    [Route($"api/auth/{GlobalConstants.MultiFactorMiddlewareRelatedControllerNames.MultiFactorController}")]
    [ApiController]
    public class MultiFactorController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationService _authenticationService;

        public MultiFactorController(IHttpContextAccessor httpContextAccessor, IAuthenticationService authenticationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// 1) Generates the QR Codes in order to start the 2fa setup procedure
        /// </summary>
        /// <returns>Qrcode as link and code in case qr code does not work</returns>
        [Produces("application/json")]
        [HttpGet("get2fa-qrcode-for-setup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = GlobalConstants.Authentication.Roles.Customer)]
        public async Task<ActionResult<ObjectResponse<QRCodeResponseDto>>> Get2fa()
        {
            var response = await _authenticationService.Generate2FASetupKey();
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        /// <summary>
        /// 2) Device Registration using the QR Code produced from step 1
        /// </summary>
        /// <returns>Recovery codes</returns>
        [Produces("application/json")]
        [HttpPost("setup2fa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = GlobalConstants.Authentication.Roles.Customer)]
        public async Task<ActionResult<ObjectResponse<SetUp2FAResponseDto>>> Setup2FA(string sixDigits)
        {
            var response = await _authenticationService.Setup2FA(sixDigits);
            if (response.Success)
            {
                return Ok(response);
            }

            //return Redirect("~/Policies/PrivacyPolicy");
            return StatusCode((int)response.HttpResultCode, response);
        }

        /// <summary>
        /// 3) Authenticates the six digits provided by the authenticator app
        /// </summary>
        /// <returns>JWT token based on the user login and a refresh token</returns>
        [Produces("application/json")]
        [HttpPost("authenticate2fa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = GlobalConstants.Authentication.Roles.Customer)]
        public async Task<ActionResult<string>> Authenticate2FA(string sixDigits)
        {
            var response = await _authenticationService.Authenticate2FA(sixDigits);

            // redirect Page
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        /// <summary>
        /// Resets the 2FA using the recovery codes produced from step 2
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost("reset2fa-by-recovery-code")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = GlobalConstants.Authentication.Roles.Customer)]
        public async Task<ActionResult<ValueResponse<bool>>> Reset2FAByRecoveryCode(string recoveryCodes)
        {
            var response = await _authenticationService.Reset2FAByRecoveryCode(recoveryCodes);
            // redirect Page
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        /// <summary>
        /// Remove 2FA from Account
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost("remove2fa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = GlobalConstants.Authentication.Roles.Customer)]
        public async Task<ActionResult<ValueResponse<bool>>> Remove2FAFromAccount()
        {
            var response = await _authenticationService.Remove2FAFromAccount();
            // redirect Page
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);

        }

    }
}
