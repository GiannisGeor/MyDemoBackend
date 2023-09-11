using Models.Entities;

namespace Data.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetActiveProductAsync(int id);
    }
}
