using AutoMapper;
using Data.Interfaces;
using Messages;
using Serilog;
using Services.Dtos;
using Services.Interfaces;

namespace Services.Services
{
    public class StoreService : IStoreService
    {
        IStoreRepository _storeRepository;
        IMapper _storeMapper;

        public StoreService(
            IStoreRepository storeRepository,
            IMapper storeMapper)
        {
            _storeRepository = storeRepository;
            _storeMapper = storeMapper;
        }

        public async Task<ListResponse<StoreStoreCategoryAddressDto>> GetStoreStoreCategoryAddress()
        {
            ListResponse<StoreStoreCategoryAddressDto> response = new();
            try
            {
                var StoreStoreCategoryAddress = await _storeRepository.GetStoreStoreCategoryAddress();
                var dtoAfterMapping = _storeMapper.Map<List<StoreStoreCategoryAddressDto>>(StoreStoreCategoryAddress);
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetStoreStoreCategoryAddress with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetStoreStoreCategoryAddress with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }
    }
}
