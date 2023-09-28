using Models.Projections;

namespace Data.Interfaces
{
    public interface IStoreRepository
    {
        Task<List<StoreStoreCategoryAddressProjection>> GetStoreStoreCategoryAddress();
    }
}
