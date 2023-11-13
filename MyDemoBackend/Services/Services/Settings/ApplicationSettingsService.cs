using AutoMapper;
using Common.Settings;
using Messages;
using Microsoft.Extensions.Options;
using Serilog;
using Services.Interfaces.Settings;
using Services.ResponseDtos.Settings;

namespace Services.Services.Settings
{
    public class ApplicationSettingsService : IApplicationSettingsService
    {
        private readonly PasswordRequirements _passwordRequirements;
        private readonly IMapper _mapper;

        public ApplicationSettingsService(IOptions<PasswordRequirements> passwordRequirements,
             IMapper mapper)
        {
            _passwordRequirements = passwordRequirements.Value;
            _mapper = mapper;
        }

        public ObjectResponse<PasswordRequirementsResponseDto> GetPasswordRequirements()
        {
            ObjectResponse<PasswordRequirementsResponseDto> response = new();
            try
            {
                if (_passwordRequirements == null)
                {
                    response.SetHttpFailureCode($@"Failed to obtain password requirements", HttpResultCode.NotFound);
                    return response;
                }
                response.SetSuccess(_mapper.Map<PasswordRequirementsResponseDto>(_passwordRequirements));
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetPasswordRequirements with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetPasswordRequirements with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }
    }
}
