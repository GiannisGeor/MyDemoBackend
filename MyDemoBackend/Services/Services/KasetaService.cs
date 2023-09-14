using AutoMapper;
using Data.Interfaces;
using Data.Repositories;
using Messages;
using Models.Entities;
using Serilog;
using Services.Dtos;
using Services.Interfaces;

namespace Services.Services
{
    public class KasetaService : IKasetaService
    {
        IKasetaRepository _kasetaRepository;
        IMapper _kasetaMapper;

        public KasetaService(
            IKasetaRepository kasetaRepository,
            IMapper kasetaMapper)
        {
            _kasetaRepository = kasetaRepository;
            _kasetaMapper = kasetaMapper;
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

        ///// <summary>
        ///// fernei mia lista apo onomata pelaton kai ta Ids kai oi times ton kaseton pou exei enoikiasei alla kai ta onomata
        ///// apo autous pou den exoun enoikiasei
        ///// </summary>
        ///// <returns></returns>
        //public async Task<ListResponse<StoixeiaPelatiKaiEnoikiasisDto>> GetOnomataIdPelatonNullKaiTimiKaseton()
        //{
        //    ListResponse<StoixeiaPelatiKaiEnoikiasisDto> response = new();
        //    try
        //    {
        //        var OnomataIdPelatonNullKaiTimiKaseton = await _kasetaRepository.GetOnomataIdPelatonNullKaiTimiKaseton();
        //        var dtoAfterMapping = _kasetaMapper.Map<List<StoixeiaPelatiKaiEnoikiasisDto>>(OnomataIdPelatonNullKaiTimiKaseton);
        //        response.SetSuccess(dtoAfterMapping);
        //        return response;
        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error(e, $@"Error while executing GetOnomataIdPelatonKaiTimiKaseton with message : {e.Message} ");
        //        response.SetHttpFailureCode($@"Error while executing GetOnomataIdPelatonKaiTimiKaseton with message : {e.Message}", HttpResultCode.InternalServerError);
        //        return response;
        //    }
        //}

        /// <summary>
        /// gyrnaei apo thn basi to Id ths kasetas tipou VHS me megaliteri posotita apo thn kaseta tipou DVDs
        /// </summary>
        /// <returns></returns>
        public async Task<ObjectResponse<List<int>>> GetIdVhsMegaliterisPosotitas()
        {
            ObjectResponse<List<int>> response = new();
            try
            {
                var IdMegaliterisPosotitas = await _kasetaRepository.GetIdVhsMegaliterisPosotitas();
                //var dtoAfterMapping = _kasetaMapper.Map<MegistiTimiDto>(megistiTimiKaseton);
                response.SetSuccess(IdMegaliterisPosotitas);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetIdVhsMegaliterisPosotitas with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetIdVhsMegaliterisPosotitas with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }

        /// <summary>
        /// fernei mia lista apo Id kaseton pou exoun Tipo VHS me diathesimi posotita megaliteri tou 2 h timi megaliteri tou 2
        /// </summary>
        /// <returns></returns>
        public async Task<ValueResponse<decimal>> GetMegistiTimiKasetas()
        {
            ValueResponse<decimal> response = new();
            try
            {
                var megistiTimiKasetas = await _kasetaRepository.GetMegistiTimiKasetas();
                response.SetSuccess(megistiTimiKasetas);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetMegistiTimiKasetas with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetMegistiTimiKasetas with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }
    }
}