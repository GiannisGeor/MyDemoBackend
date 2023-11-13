using Common.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Auth;

namespace Api.Controllers.Auth.Basic_Authentication
{
    [Route("api/auth/refresh")]
    [ApiController]
    public class RefreshController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationService _authenticationService;

        public RefreshController(IHttpContextAccessor httpContextAccessor, IAuthenticationService authenticationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Refresh Access Token
        /// </summary>
        /// <returns> JWT Access token</returns>
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Refresh()
        {
            // Read value from cookie
            var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies[GlobalConstants.Authentication.Cookies.RefreshTokenCookie];
            if (refreshToken == null)
            {
                // No refresh token found
                return Unauthorized();
            }

            // Request new access token
            var response = await _authenticationService.GenerateAccessToken(refreshToken);
            if (response.Success)
            {
                return Ok(response.Item);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}
