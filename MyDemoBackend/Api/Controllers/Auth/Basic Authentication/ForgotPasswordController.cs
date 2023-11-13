using Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Auth;

namespace Api.Controllers.Auth.Basic_Authentication
{
    [Route("api/auth/forgot-password")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationService _authenticationService;

        public ForgotPasswordController(IHttpContextAccessor httpContextAccessor, IAuthenticationService authenticationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// 1) Sends the required information with email to the user in order to resets his password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost("forgot-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ValueResponse<bool>>> ForgotPassword(string email)
        {
            ValueResponse<bool> response = await _authenticationService.ForgotPassword(email);

            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        /// <summary>
        /// 2) Uses the information that were sent from step 1 to reset users email
        /// </summary>
        /// <param name="authUserId"></param>
        /// <param name="resetToken"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ValueResponse<bool>>> ResetPassword(Guid authUserId, string resetToken, string newPassword)
        {
            ValueResponse<bool> response = await _authenticationService.ResetPassword(authUserId, newPassword, resetToken);

            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}
