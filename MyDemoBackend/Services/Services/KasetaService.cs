using Data.Interfaces;
using Messages;
using Serilog;
using Services.Interfaces;

namespace Services.Services
{
    public class KasetaService : IKasetaService
    {
        IKasetaRepository _kasetaRepository;

        public KasetaService(
            IKasetaRepository kasetaRepository)
        {
            _kasetaRepository = kasetaRepository;
        }

        /// <summary>
        /// fernei mia lista apo Id kaseton pou exoun Tipo VHS me diathesimi posotita megaliteri tou 2 h timi megaliteri tou 2
        /// </summary>
        /// <returns></returns>
        public async Task<ObjectResponse<List<int>>> GetKasetaIdVhs()
        {
            ObjectResponse<List<int>> response = new ObjectResponse<List<int>>();
            try
            {
                List<int> IdKaseton = await _kasetaRepository.GetKasetaIdVhs();
                response.SetSuccess(IdKaseton);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetKasetaIdVhs with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetKasetaIdVhs with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }

        /// <summary>
        /// fernei mia lista apo Id kaseton taksinomimeni me auksonta tropo os pros thn posotita
        /// </summary>
        /// <returns></returns>
        public async Task<ObjectResponse<List<int>>> GetKasetaIdAscend()
        {
            ObjectResponse<List<int>> response = new ObjectResponse<List<int>>();
            try
            {
                List<int> IdKasetonAscend = await _kasetaRepository.GetKasetaIdAscend();
                response.SetSuccess(IdKasetonAscend);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetKasetaIdAscend with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetKasetaIdAscend with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }

        /// <summary>
        /// fernei mia lista apo Id kaseton taksinomimeni me fthinonta tropo os pros thn timi
        /// </summary>
        /// <returns></returns>
        public async Task<ObjectResponse<List<int>>> GetKasetaIdDescend()
        {
            ObjectResponse<List<int>> response = new ObjectResponse<List<int>>();
            try
            {
                List<int> IdKasetonDescend = await _kasetaRepository.GetKasetaIdDescend();
                response.SetSuccess(IdKasetonDescend);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetKasetaIdDescend with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetKasetaIdDescend with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }

        /// <summary>
        /// fernei mia lista apo 2 Id kaseton me thn megaliteri posotita
        /// </summary>
        /// <returns></returns>
        public async Task<ObjectResponse<List<int>>> GetIdDioKaseton()
        {
            ObjectResponse<List<int>> response = new ObjectResponse<List<int>>();
            try
            {
                List<int> GetIdDioKaseton = await _kasetaRepository.GetIdDioKaseton();
                response.SetSuccess(GetIdDioKaseton);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetIdDioKaseton with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetIdDioKaseton with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }
    }
}