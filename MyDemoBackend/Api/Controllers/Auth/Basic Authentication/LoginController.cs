using Common.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos.Auth;
using Services.Interfaces.Auth;

namespace Api.Controllers.Auth.Basic_Authentication
{
    [Route($"api/auth/{GlobalConstants.MultiFactorMiddlewareRelatedControllerNames.LoginController}")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;

        public LoginController(IHttpContextAccessor httpContextAccessor,
            IAuthenticationService authenticationService,
            IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        /// <summary>
        /// Login as forms user
        /// </summary>
        /// <returns>JWT token based on the user login and a refresh token</returns>
        [Produces("application/json")]
        [HttpPost("login-forms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<string>> LoginForms([FromBody] LoginDto loginDto)
        {
            var response = await _authenticationService.LoginForms(loginDto);
            if (response.Success)
            {
                return Ok(response);
            }

            //return Redirect("~/Policies/PrivacyPolicy");
            return StatusCode((int)response.HttpResultCode, response);
        }

        /// <summary>
        /// Test method with authorize attribute for checking if auth works (Without MultiFactorMiddleware interference)
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult> Ping()
        {
            return Ok();
        }
    }
}
