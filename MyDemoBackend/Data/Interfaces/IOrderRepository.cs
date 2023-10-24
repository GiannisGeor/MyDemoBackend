using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddNewOrder(Order candidate);
    }
}
