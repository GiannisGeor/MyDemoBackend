using Messages;
using Models.Projections;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface IStoreService
    {

        Task<ListResponse<StoreStoreCategoryAddressDto>> GetStoreStoreCategoryAddress();
        Task<ObjectResponse<AllInitialDataDto>> GetAllInitialData(int id);
    }
}
