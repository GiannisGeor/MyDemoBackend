using AutoMapper;
using Data.Interfaces;
using Messages;
using Models.Entities;
using Models.Projections;
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

        /// <summary>
        /// fernei mia lista apo IdKaseta IdPelati kai Tis imerominies opou enoikiastike h kaseta kai
        /// null stis imerominies epistrofis
        /// </summary>
        /// <returns></returns>
        public async Task<ListResponse<EnoikiasiDto>> GetEnoikiaseisNull()
        {
            ListResponse<EnoikiasiDto> response = new ListResponse<EnoikiasiDto>();
            try
            {
                List<Enoikiasi> enoikiaseisNull = await _enoikiasiRepository.GetEnoikiaseisNull();
                var dtoAfterMapping = _enoikiasiMapper.Map<List<EnoikiasiDto>>(enoikiaseisNull);
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetEnoikiaseisNull with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetEnoikiaseisNull with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// fernei mia lista apo Id Kaseton kai tis imerominies opou epistrafikan oi kasetes
        /// </summary>
        /// <returns></returns>
        public async Task<ListResponse<EpistrofiKasetasDto>> GetIdKasetonEnoikiasmenon()
        {
            ListResponse<EpistrofiKasetasDto> response = new();
            try
            {
                List<EpistrofiKasetasProjection> idKasetonEnoikiasmenon = await _enoikiasiRepository.GetIdKasetonEnoikiasmenon();
                var dtoAfterMapping = _enoikiasiMapper.Map<List<EpistrofiKasetasDto>>(idKasetonEnoikiasmenon);
                response.SetSuccess(dtoAfterMapping);
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
