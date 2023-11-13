using Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces.Auth;

namespace Api.Controllers.Auth.Basic_Authentication
{
    [Route("api/auth/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public RegisterController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Register forms user and sends confirmation email in the case the backend requires a confirmed email
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost("registration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Registration([FromBody] RegisterNewUserDto newUser)
        {
            ValueResponse<bool> response = await _authenticationService.RegisterNewUser(newUser);

            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}
