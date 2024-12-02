using Siruis_Project.Core.Dtos.OrderDto;
using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.ServiceContract
{
    public interface IOrderServices
    {
        Task<IEnumerable<Order>> GetAllOrders();


        Task<Order> GetOrderById(int id);


        Task<Order> AddOrder(OrderAddReq order);


        Task DeleteOrder(int id);

        Task<Order> UpdateOrder(OrderUpdateReq order);
        Task UpdateStatus(OrderUpdateStatusReq statusReq);

        Task<bool> DeleteAllOrders();

    }
}
