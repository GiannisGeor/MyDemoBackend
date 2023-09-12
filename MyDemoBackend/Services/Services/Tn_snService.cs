using AutoMapper;
using Data.Interfaces;
using Messages;
using Serilog;
using Services.Dtos;
using Services.Interfaces;

namespace Services.Services
{
    public class Tn_snService : ITn_snService
    {
        ITn_snRepository _tn_snRepository;
        IMapper _tn_snMapper;
        public Tn_snService(
            ITn_snRepository tn_snRepository,
            IMapper tn_snMapper)
        {
            _tn_snRepository = tn_snRepository;
            _tn_snMapper = tn_snMapper;
        }

        /// <summary>
        /// fernei mia lista apo onomata kai rolous ton anitstoixon sinteleston
        /// </summary>
        /// <returns></returns>
        public async Task<ListResponse<OnomaKaiRolosSintelestiDto>> GetOnomataRolousSinteleston()
        {
            ListResponse<OnomaKaiRolosSintelestiDto> response = new();
            try
            {
                var OnomataRoloiSinteleston = await _tn_snRepository.GetOnomataRolousSinteleston();
                var dtoAfterMapping = _tn_snMapper.Map<List<OnomaKaiRolosSintelestiDto>>(OnomataRoloiSinteleston);
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetOnomataRolousSinteleston with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetOnomataRolousSinteleston with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// fernei mia lista apo Ids tainion opou simetexei o Alfred Hitchcock
        /// </summary>
        /// <returns></returns>
        public async Task<ObjectResponse<List<int>>> GetTainiaIdAlfred()

        {
            ObjectResponse<List<int>> response = new ObjectResponse<List<int>>();
            try
            {
                List<int> TainiaIdAlfred = await _tn_snRepository.GetTainiaIdAlfred();
                response.SetSuccess(TainiaIdAlfred);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetTainiaIdAlfred with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetTainiaIdAlfred with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }
    }
}
