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
        Task<IEnumerable<OrderUpdateReq>> GetAllOrders();


        Task<OrderUpdateReq> GetOrderById(int id);


        Task<OrderUpdateReq> AddOrder(OrderAddReq order);


        Task<bool> DeleteOrder(int id);

        Task<OrderUpdateReq> UpdateOrder(OrderUpdateReq order);
        Task<OrderUpdateStatusReq> UpdateStatus(OrderUpdateStatusReq statusReq);

        Task<bool> DeleteAllOrders();

    }
}
