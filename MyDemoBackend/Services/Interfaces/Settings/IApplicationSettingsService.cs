using Messages;
using Services.ResponseDtos.Settings;

namespace Services.Interfaces.Settings
{
    public interface IApplicationSettingsService
    {
        /// <summary>
        /// Obtains the password requirements from backend application settings and provides them for frontend use
        /// </summary>
        /// <returns></returns>
        ObjectResponse<PasswordRequirementsResponseDto> GetPasswordRequirements();

    }
}
