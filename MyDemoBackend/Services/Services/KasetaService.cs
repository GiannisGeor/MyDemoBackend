using System.Collections.Generic;
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


        public KasetaService(
            IKasetaRepository kasetaRepository)
        {
            _kasetaRepository = kasetaRepository;
        }

        public async Task<ObjectResponse<List<int>>> GetKasetaId()
        {
            ObjectResponse<List<int>> response = new ObjectResponse<List<int>>();
            try
            {
                List<int> IdKaseton = await _kasetaRepository.GetKasetaId();
                response.SetSuccess(IdKaseton);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetKasetaId with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetKasetaId with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }
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
                Log.Error(e, $@"Error while executing GetKasetaIdDescend with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetKasetaIdDescend with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }
    }
}