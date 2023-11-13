using Common.Configuration;
using Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Auth;
using static Common.Configuration.GlobalConstants.Authentication;

namespace Api.Controllers.Auth.Basic_Authentication
{
    [Route("api/auth/change-password")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationService _authenticationService;

        public ChangePasswordController(IHttpContextAccessor httpContextAccessor, IAuthenticationService authenticationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticationService = authenticationService;
        }


        /// <summary>
        /// Change user password.
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = GlobalConstants.Authentication.Roles.Customer)]
        public async Task<ActionResult<ValueResponse<bool>>> ChangePassword(string currentPassword, string newPassword)
        {
            var response = await _authenticationService.ChangePassword(currentPassword, newPassword);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);


        }


    }
}
