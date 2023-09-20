using AutoMapper;
using Data.Interfaces;
using FluentValidation;
using Messages;
using Models.Entities;
using Serilog;
using Services.Dtos;
using Services.Interfaces;

namespace Services.Services
{
    public class TainiaService : ITainiaService
    {
        private readonly ITainiaRepository _tainiaRepository;
        private readonly IMapper _tainiaMapper;
        private readonly IValidator<NeaTainiaDto> _neaTainiaValidator;
        private readonly IValidator<MetaboliTainiasDto> _metaboliTainiasValidator;
        private readonly IValidator<SintelestesKaiRoloiDto> _sintelestesKaiRoloiValidator;
        private readonly IValidator<NeaTainiaKaiSintelestesDto> _neaTainiaKaiSintelestesValidator;
        private readonly IValidator<ProsthikiSintelestonDto> _prosthikiSintelestonValidator;

        public TainiaService(
            ITainiaRepository tainiaRepository,
            IMapper tainiaMapper,
            IValidator<NeaTainiaDto> neaTainiaValidator,
            IValidator<MetaboliTainiasDto> metaboliTainiasValidator,
            IValidator<SintelestesKaiRoloiDto> sintelestesKaiRoloiValidator,
            IValidator<NeaTainiaKaiSintelestesDto> neaTainiaKaiSintelestesValidator,
            IValidator<ProsthikiSintelestonDto> prosthikiSintelestonValidator)
        {
            _tainiaRepository = tainiaRepository;
            _tainiaMapper = tainiaMapper;
            _neaTainiaValidator = neaTainiaValidator;
            _metaboliTainiasValidator = metaboliTainiasValidator;
            _sintelestesKaiRoloiValidator = sintelestesKaiRoloiValidator;
            _neaTainiaKaiSintelestesValidator = neaTainiaKaiSintelestesValidator;
            _prosthikiSintelestonValidator = prosthikiSintelestonValidator;
        }

        public async Task<ObjectResponse<NeaTainiaResponseDto>> NeaTainia(NeaTainiaDto neaTainiaDto)
        {
            ObjectResponse<NeaTainiaResponseDto> response = new();
            try
            {
                var validationResult = _neaTainiaValidator.Validate(neaTainiaDto);
                if (!validationResult.IsValid)
                {
                    response.SetFailureWithValidation(validationResult);
                    return response;
                }
                Tainia candidate = _tainiaMapper.Map<Tainia>(neaTainiaDto);
                candidate.MarkNew();
                Tainia entity = await _tainiaRepository.AddNewTainia(candidate);
                var dtoAfterMapping = _tainiaMapper.Map<NeaTainiaResponseDto>(entity);
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing NeaTainia with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing NeaTainia with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ObjectResponse<MetaboliTainiasResponseDto>> MetaboliTainias(int id, MetaboliTainiasDto metaboliTainiasDto)
        {
            ObjectResponse<MetaboliTainiasResponseDto> response = new();
            try
            {

                var validationResult = _metaboliTainiasValidator.Validate(metaboliTainiasDto);
                if (!validationResult.IsValid)
                {
                    response.SetFailureWithValidation(validationResult);
                    return response;
                }

                Tainia candidateTainia = await _tainiaRepository.GetTainiaUnTrackedById(id);
                if (candidateTainia == null)
                {
                    response.SetHttpFailureCode($@"Auth h tainia den yparxei", HttpResultCode.NotFound);
                    return response;
                }

                candidateTainia.Xronia = metaboliTainiasDto.Xronia;
                candidateTainia.Titlos = metaboliTainiasDto.Titlos;

                Tainia entity = await _tainiaRepository.MetaboliTainias(candidateTainia);
                var dtoAfterMapping = _tainiaMapper.Map<MetaboliTainiasResponseDto>(entity);
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing MetaboliTainias with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing MetaboliTainias with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ObjectResponse<NeaTainiaKaiSintelestesResponseDto>> NeaTainiaKaiSintelestes(NeaTainiaKaiSintelestesDto neaTainiaKaiSintelestesDto)
        {
            ObjectResponse<NeaTainiaKaiSintelestesResponseDto> response = new();
            try
            {
                var validationResult = _neaTainiaKaiSintelestesValidator.Validate(neaTainiaKaiSintelestesDto);
                if (!validationResult.IsValid)
                {
                    response.SetFailureWithValidation(validationResult);
                    return response;
                }
                Tainia candidate = new Tainia();
                candidate.Titlos = neaTainiaKaiSintelestesDto.Titlos;
                candidate.Xronia = neaTainiaKaiSintelestesDto.Xronia;
                candidate.MarkNew();

                foreach (var item in neaTainiaKaiSintelestesDto.SintelestesKaiRoloi)
                {
                    Sintelestis candidateSintelestis = new Sintelestis();
                    candidateSintelestis.Onoma = item.Onoma;
                    candidateSintelestis.MarkNew();

                    Tn_sn candidateTn_sn = new Tn_sn();
                    candidateTn_sn.Sintelestis = candidateSintelestis;
                    candidateTn_sn.Rolos = item.Rolos;

                    candidate.TainiesSintelestes.Add(candidateTn_sn);
                }

                Tainia entity = await _tainiaRepository.AddNewTainiaKaiSintelestes(candidate);
                var dtoAfterMapping = _tainiaMapper.Map<NeaTainiaKaiSintelestesResponseDto>(entity);
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing NeaTainiaKaiSintelestes with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing NeaTainiaKaiSintelestes with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ObjectResponse<ProsthikiSintelestonResponseDto>> ProsthikiSinteleston(int id, ProsthikiSintelestonDto prosthikiSintelestonDto)
        {
            ObjectResponse<ProsthikiSintelestonResponseDto> response = new();
            try
            {
                var validationResult = _prosthikiSintelestonValidator.Validate(prosthikiSintelestonDto);
                if (!validationResult.IsValid)
                {
                    response.SetFailureWithValidation(validationResult);
                    return response;
                }

                Tainia candidateTainia = await _tainiaRepository.GetTainiaUnTrackedById(id);
                if (candidateTainia == null)
                {
                    response.SetHttpFailureCode($@"Auth h tainia den yparxei", HttpResultCode.NotFound);
                    return response;
                }

                foreach (var item in prosthikiSintelestonDto.SintelestesKaiRoloi)
                {
                    Sintelestis candidateSintelestis = new Sintelestis();
                    candidateSintelestis.Onoma = item.Onoma;
                    candidateSintelestis.MarkNew();

                    Tn_sn candidateTn_sn = new Tn_sn();
                    candidateTn_sn.Sintelestis = candidateSintelestis;
                    candidateTn_sn.Rolos = item.Rolos;

                    candidateTainia.TainiesSintelestes.Add(candidateTn_sn);
                }

                Tainia entity = await _tainiaRepository.AddSintelestesSeTainies(candidateTainia);
                var dtoAfterMapping = _tainiaMapper.Map<ProsthikiSintelestonResponseDto>(entity);
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing MetaboliTainias with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing MetaboliTainias with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }
    }
}

