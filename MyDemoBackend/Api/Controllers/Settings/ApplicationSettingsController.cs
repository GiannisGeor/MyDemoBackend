using Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Settings;
using Services.ResponseDtos.Settings;

namespace Api.Controllers.Settings
{
    [Route("api/application-settings")]
    [ApiController]
    public class ApplicationSettingsController : ControllerBase
    {
        private readonly IApplicationSettingsService _applicationSettings;

        public ApplicationSettingsController(IApplicationSettingsService applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }
        /// <summary>
        /// Obtains the password requirements from backend application settings and provides them for frontend use
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("password-requirements")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ObjectResponse<PasswordRequirementsResponseDto>>> PasswordRequirements()
        {
            ObjectResponse<PasswordRequirementsResponseDto> response = _applicationSettings.GetPasswordRequirements();

            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode((int)response.HttpResultCode, response);
        }
    }
}
