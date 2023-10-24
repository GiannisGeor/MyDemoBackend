using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using Models.Entities;

namespace Data.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetIdProducts(List<int> requestedProducts);
    }
}
