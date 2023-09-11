using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class EnoikiasiService : IEnoikiasiService
    {
        IEnoikiasiRepository _enoikiasiRepository;
        IMapper _enoikiasiMapper;


        public EnoikiasiService(
            IEnoikiasiRepository enoikiasiRepository,
            IMapper enoikiasiMapper)
        {
            _enoikiasiRepository = enoikiasiRepository;
            _enoikiasiMapper = enoikiasiMapper;
        }

        public async Task<ListResponse<EnoikiasiDto>> GetEnoikiaseis()
        {
            ListResponse<EnoikiasiDto> response = new ListResponse<EnoikiasiDto>();
            try
            {
                List<Enoikiasi> enoikiaseis = await _enoikiasiRepository.GetEnoikiaseis();
                var dtoAfterMapping = _enoikiasiMapper.Map<List<EnoikiasiDto>>(enoikiaseis);

                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetEnoikiaseis with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetEnoikiaseis with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ObjectResponse<List<Tuple<int, int, int, int>>>> GetIdKasetonEnoikiasmenon()
        {
            ObjectResponse<List<Tuple<int, int, int, int>>> response = new ObjectResponse<List<Tuple<int, int, int, int>>>();
            try
            {
                List<Tuple<int, int, int, int>> IdKasetonEnoikiasmenon = await _enoikiasiRepository.GetIdKasetonEnoikiasmenon();
                response.SetSuccess(IdKasetonEnoikiasmenon);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetKasetaIdAscend with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetKasetaIdAscend with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }

        }
    }
}
