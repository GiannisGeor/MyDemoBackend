using Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Auth;

namespace Api.Controllers.Auth.Basic_Authentication
{
    [Route("api/auth/email-confirmation")]
    [ApiController]
    public class EmailConfirmationController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationService _authenticationService;

        public EmailConfirmationController(IHttpContextAccessor httpContextAccessor,
            IAuthenticationService authenticationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Confirms User Email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="emailToken"></param>
        /// <returns>ValueResponse with bool</returns>
        [Produces("application/json")]
        [HttpPost("confirm-userid/{userId}/token/{emailToken}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ValueResponse<bool>>> ConfirmEmail([FromRoute] Guid userId, [FromRoute] string emailToken)
        {
            ValueResponse<bool> response = await _authenticationService.ConfirmEmail(userId, emailToken);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }

        /// <summary>
        /// Resends the confirmation email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>ValueResponse with bool</returns>
        [Produces("application/json")]
        [HttpPost("resend-confirmation-email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ValueResponse<bool>>> ResendConfirmationEmail([FromRoute] string email)
        {
            ValueResponse<bool> response = await _authenticationService.ResendConfirmationEmail(email);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}
