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
    public class PelatisService : IPelatisService
    {
        IPelatisRepository _pelatisRepository;
        IMapper _pelatisMapper;
        private readonly IValidator<NeosPelatisDto> _neosPelatisValidator;


        public PelatisService(
            IPelatisRepository pelatisRepository,
            IMapper pelatisMapper,
            IValidator<NeosPelatisDto> neosPelatisValidator)
        {
            _pelatisRepository = pelatisRepository;
            _pelatisMapper = pelatisMapper;
            _neosPelatisValidator = neosPelatisValidator;
        }

        /// <summary>
        /// fernei mia lista apo ola ta onomata
        /// </summary>
        /// <returns></returns>
        public async Task<ListResponse<string>> GetOnomata()
        {
            ListResponse<string> response = new ListResponse<string>();
            try
            {
                List<string> onomataPelaton = await _pelatisRepository.GetOnomata();
                response.SetSuccess(onomataPelaton);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetOnomata with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetOnomata with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }

        /// <summary>
        /// fernei mia lista apo ta tilefona olon ton pelaton
        /// </summary>
        /// <returns></returns>
        public async Task<ListResponse<string>> GetTilefona()
        {
            ListResponse<string> response = new ListResponse<string>();
            try
            {
                List<string> tilefona = await _pelatisRepository.GetTilefona();
                response.SetSuccess(tilefona);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetTilefona with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetTilefona with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// fernei mia lista apo ola ta stoixeia ton pelaton
        /// </summary>
        /// <returns></returns>
        public async Task<ListResponse<PelatisDto>> GetPelates()
        {
            ListResponse<PelatisDto> response = new ListResponse<PelatisDto>();
            try
            {
                List<Pelatis> pelates = await _pelatisRepository.GetPelates();
                var dtoAfterMapping = _pelatisMapper.Map<List<PelatisDto>>(pelates);

                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetPelates with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetPelates with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// fernei mia lista apo ta tilefona olon ton pelaton mazi me to prothema '2310'
        /// </summary>
        /// <returns></returns>
        public async Task<ListResponse<string>> GetTilefonaMeKodikous1()
        {
            ListResponse<string> response = new ListResponse<string>();
            try
            {
                List<string> tilefonaMeKodikous = await _pelatisRepository.GetTilefonaMeKodikous();
                response.SetSuccess(tilefonaMeKodikous);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetTilefonaMeKodikous1 with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetTilefonaMeKodikous1 with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// fernei mia lista apo ta tilefona olon ton pelaton mazi me to prothema '2310'
        /// </summary>
        /// <returns></returns>
        public async Task<ListResponse<string>> GetTilefonaMeKodikous2()
        {
            ListResponse<string> response = new ListResponse<string>();
            try
            {
                List<string> tilefona = await _pelatisRepository.GetTilefona();
                List<string> tilefonaMeKodikous2 = tilefona.Select(x => "2310" + x).ToList();
                response.SetSuccess(tilefonaMeKodikous2);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetTilefonaMeKodikous2 with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetTilefonaMeKodikous2 with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }

        /// <summary>
        /// fernei mia lista apo ola ta onomata pou arxizoun apo K
        /// </summary>
        /// <returns></returns>
        public async Task<ListResponse<string>> GetOnomataApoK()
        {
            ListResponse<string> response = new ListResponse<string>();
            try
            {
                List<string> onomataPelaton = await _pelatisRepository.GetOnomata();
                List<string> OnomataApoK = onomataPelaton
                    .Where(x => x != null && x.StartsWith("K"))
                    .ToList();
                response.SetSuccess(OnomataApoK);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetTilefonaMeKodikous2 with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetTilefonaMeKodikous2 with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }

        /// <summary>
        /// fernei mia lista apo onomata pelaton kai ta Ids kai oi times ton kaseton pou exei enoikiasei  
        /// </summary>
        /// <returns></returns>
        public async Task<ListResponse<StoixeiaPelatiKaiEnoikiasisDto>> GetOnomataIdPelatonKaiTimiKaseton()
        {
            ListResponse<StoixeiaPelatiKaiEnoikiasisDto> response = new();
            try
            {
                var OnomataIdPelatonKaiTimiKaseton = await _pelatisRepository.GetOnomataIdPelatonKaiTimiKaseton();
                var dtoAfterMapping = _pelatisMapper.Map<List<StoixeiaPelatiKaiEnoikiasisDto>>(OnomataIdPelatonKaiTimiKaseton);
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetOnomataIdPelatonKaiTimiKaseton with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetOnomataIdPelatonKaiTimiKaseton with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// fernei mia lista apo onomata pelaton kai ta Ids kai oi times ton kaseton pou exei enoikiasei alla kai ta onomata
        /// apo autous pou den exoun enoikiasei
        /// </summary>
        /// <returns></returns>
        public async Task<ListResponse<StoixeiaPelatiKaiEnoikiasisDto>> GetOnomataIdPelatonKaiTimiKasetonNull()
        {
            ListResponse<StoixeiaPelatiKaiEnoikiasisDto> response = new();
            try
            {
                var OnomataIdPelatonKaiTimiKasetonNull = await _pelatisRepository.GetOnomataIdPelatonKaiTimiKasetonNull();
                var dtoAfterMapping = _pelatisMapper.Map<List<StoixeiaPelatiKaiEnoikiasisDto>>(OnomataIdPelatonKaiTimiKasetonNull);
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetOnomataIdPelatonKaiTimiKaseton with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetOnomataIdPelatonKaiTimiKaseton with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ObjectResponse<NeosPelatisResponseDto>> NeosPelatis(NeosPelatisDto neosPelatisDto)
        {
            ObjectResponse<NeosPelatisResponseDto> response = new();
            try
            {
                var validationResult = _neosPelatisValidator.Validate(neosPelatisDto);
                if (!validationResult.IsValid)
                {
                    response.SetFailureWithValidation(validationResult);
                    return response;
                }
                Pelatis candidate = _pelatisMapper.Map<Pelatis>(neosPelatisDto);
                candidate.MarkNew();
                Pelatis entity = await _pelatisRepository.AddNewPelatis(candidate);
                var dtoAfterMapping = _pelatisMapper.Map<NeosPelatisResponseDto>(entity);
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
    }
}
