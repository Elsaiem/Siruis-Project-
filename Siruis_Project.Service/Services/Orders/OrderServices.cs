using Microsoft.EntityFrameworkCore;
using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.OrderDto;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Service.Services.Orders
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of Work is not initialized.");
            }

            var OrderRepository = _unitOfWork.Repository<Order>();
            if (OrderRepository == null)
            {
                throw new InvalidOperationException("Client repository is not initialized.");
            }

            var result = await OrderRepository.GetAllAsync();
            return result ?? Enumerable.Empty<Order>();
        }
        public async Task<Order> GetOrderById(int id)
        {
            var order = await _unitOfWork.Repository<Order>().GetAsync(id);
            if (order == null) return null;
            return order;
        }



        public async Task<Order> AddOrder(OrderAddReq order)
        {
            if (order == null) return null;

            var response = new Order
            {
                Name= order.Name,
                Phone= order.Phone,
                Address=order.Address,
                Govern=order.Govern,
                Ads=order.Ads,
                Animation=order.Animation,
                Branding=order.Branding,
                CopyWriting=order.CopyWriting,
                Design=order.Design,
                DigitalCamaign=order.DigitalCamaign,
                Moderation=order.Moderation,
                ModerationHour=order.ModerationHour,
                Photography=order.Photography,
                plan=order.plan,
                Platform=order.Platform,
                Reels=order.Reels,
                Stories=order.Stories,
                voiceOver=order.voiceOver


            };

            await _unitOfWork.Repository<Order>().AddAsync(response);
            await _unitOfWork.CompleteAsync();
            return response;
        }

        public async Task<bool> DeleteAllOrders()
        {
            try
            {
                // Fetch all orders
                var orders = await _unitOfWork.Repository<Order>().GetAllAsync();

                // Check if there are no orders to delete
                if (orders == null || !orders.Any())
                    return false;

                // Remove all orders
                _unitOfWork.Repository<Order>().DeleteAll();
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch
            {
              
                return false; 
            }
        }



        public async Task DeleteOrder(int id)
        {
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of Work is not initialized.");
            }

            var OrderRepository = _unitOfWork.Repository<Order>();
            if (OrderRepository == null)
            {
                throw new InvalidOperationException("Order repository is not initialized.");
            }

            var result = await OrderRepository.GetAsync(id);

            if (result == null)
            {
                throw new InvalidOperationException($"Order with id {id} does not exist.");
            }

            OrderRepository.Delete(result);
            await _unitOfWork.CompleteAsync(); // Save the changes
        }
        public async Task UpdateStatus(OrderUpdateStatusReq statusReq)
        {
            var check= await _unitOfWork.Repository<Order>().GetAsync(statusReq.Id);
            if (check == null) throw new InvalidOperationException($"Order with id {statusReq.Id} does not exist.");

            check.status = statusReq.Status;
            await _unitOfWork.Repository<Order>().Update(check);
            await _unitOfWork.CompleteAsync();

        }



        public async Task<Order> UpdateOrder(OrderUpdateReq order)
        {
            var check = await _unitOfWork.Repository<Order>().GetAsync(order.Id);
            if (check is null) return null;

            // Update properties explicitly
           

            check.Name = order.Name;
            check.Phone = order.Phone;
            check.Address = order.Phone;
            check.Ads = order.Ads;
            check.Animation = order.Animation;
            check.Branding = order.Branding;
            check.CopyWriting = order.CopyWriting;
            check.Design = order.Design;
            check.DigitalCamaign = order.DigitalCamaign;
            check.Govern = order.Govern;
            check.Moderation = order.Moderation;
            check.ModerationHour = order.ModerationHour;
            check.Photography = order.Photography;
            check.plan = order.plan;
            check.Platform = order.Platform;
            check.Reels = order.Reels;
            check.status = order.status;
            check.Stories = order.Stories;
            check.voiceOver = order.voiceOver;


            await _unitOfWork.Repository<Order>().Update(check);
            await _unitOfWork.CompleteAsync();
            return check; // Return the updated entity
        }
    }
}
