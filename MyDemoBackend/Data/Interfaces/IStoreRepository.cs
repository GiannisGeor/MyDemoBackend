using Models.Entities;
using Models.Projections;

namespace Data.Interfaces
{
    public interface IStoreRepository
    {
        Task<List<StoreStoreCategoryAddressProjection>> GetStoreStoreCategoryAddress();
        Task<AllInitialDataProjection> GetAllInitialData(int id);        
    }
}
