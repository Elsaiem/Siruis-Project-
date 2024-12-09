using Microsoft.EntityFrameworkCore;
using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.ClientDto;
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

        public async Task<IEnumerable<OrderUpdateReq>> GetAllOrders()
        {
            try
            {
                var orderRepository = _unitOfWork.Repository<Order>();
                var orders = await orderRepository.GetAllAsync();
                if (orders == null || !orders.Any())
                    return Enumerable.Empty<OrderUpdateReq>();

                var response = orders.Select(order => new OrderUpdateReq
                {
                    Id = order.Id,
                    Name = order.Name,
                    Phone = order.Phone,
                    Address = order.Address,
                    Govern = order.Govern,
                    Ads = order.Ads,
                    Animation = order.Animation,
                    Branding = order.Branding,
                    CopyWriting = order.CopyWriting,
                    Design = order.Design,
                    DigitalCamaign = order.DigitalCamaign,
                    Moderation = order.Moderation,
                    ModerationHour = order.ModerationHour,
                    Photography = order.Photography,
                    plan = order.plan,
                    Platform = order.Platform,
                    Reels = order.Reels,
                    Stories = order.Stories,
                    voiceOver = order.voiceOver
                });

                return response;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while retrieving all Orders.");
                throw new InvalidOperationException("An error occurred while retrieving orders.", ex);
            }
        }

        public async Task<OrderUpdateReq?> GetOrderById(int id)
        {
            try
            {
                var order = await _unitOfWork.Repository<Order>().GetAsync(id);
                if (order is null) return null;
                var response = new OrderUpdateReq
                {
                    Id = order.Id,
                    Name = order.Name,
                    Phone = order.Phone,
                    Address = order.Address,
                    Govern = order.Govern,
                    Ads = order.Ads,
                    Animation = order.Animation,
                    Branding = order.Branding,
                    CopyWriting = order.CopyWriting,
                    Design = order.Design,
                    DigitalCamaign = order.DigitalCamaign,
                    Moderation = order.Moderation,
                    ModerationHour = order.ModerationHour,
                    Photography = order.Photography,
                    plan = order.plan,
                    Platform = order.Platform,
                    Reels = order.Reels,
                    Stories = order.Stories,
                    voiceOver = order.voiceOver

                };

                return response ?? null;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while retrieving a Order by ID.");
                throw new InvalidOperationException($"An error occurred while retrieving the Order with ID {id}.", ex);
            }
        }




        public async Task<OrderUpdateReq> AddOrder(OrderAddReq order)
        {
            try
            {
                if (order == null)
                    throw new ArgumentNullException(nameof(order), "Order data is null.");

                var response = new Order
                {
                    Name = order.Name,
                    Phone = order.Phone,
                    Address = order.Address,
                    Govern = order.Govern,
                    Ads = order.Ads,
                    Animation = order.Animation,
                    Branding = order.Branding,
                    CopyWriting = order.CopyWriting,
                    Design = order.Design,
                    DigitalCamaign = order.DigitalCamaign,
                    Moderation = order.Moderation,
                    ModerationHour = order.ModerationHour,
                    Photography = order.Photography,
                    plan = order.plan,
                    Platform = order.Platform,
                    Reels = order.Reels,
                    Stories = order.Stories,
                    voiceOver = order.voiceOver


                };
                await _unitOfWork.Repository<Order>().AddAsync(response);
                await _unitOfWork.CompleteAsync();

                return new OrderUpdateReq
                {
                    Id=response.Id,
                    Name = order.Name,
                    Phone = order.Phone,
                    Address = order.Address,
                    Govern = order.Govern,
                    Ads = order.Ads,
                    Animation = order.Animation,
                    Branding = order.Branding,
                    CopyWriting = order.CopyWriting,
                    Design = order.Design,
                    DigitalCamaign = order.DigitalCamaign,
                    Moderation = order.Moderation,
                    ModerationHour = order.ModerationHour,
                    Photography = order.Photography,
                    plan = order.plan,
                    Platform = order.Platform,
                    Reels = order.Reels,
                    Stories = order.Stories,
                    voiceOver = order.voiceOver

                };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while adding a Order.");
                throw new InvalidOperationException("An error occurred while adding the Order.", ex);
            }
        }

        public async Task<bool> DeleteAllOrders()
        {
            try
            {
                var orders = await _unitOfWork.Repository<Order>().GetAllAsync();
                if (orders == null || !orders.Any())
                    return false;

                _unitOfWork.Repository<Order>().DeleteAll();
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while deleting all Orders.");
                return false;
            }
        }



        public async Task<bool> DeleteOrder(int id)
        {
            try
            {
                var orderRepository = _unitOfWork.Repository<Order>();
                var order = await orderRepository.GetAsync(id);
                if (order == null)
                    return false;

                orderRepository.Delete(order);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while deleting a Order by ID.");
                return false;
            }
        }
        public async Task<OrderUpdateStatusReq> UpdateStatus(OrderUpdateStatusReq statusReq)
        {
            try
            {
                if (statusReq == null)
                    throw new ArgumentNullException(nameof(statusReq), "Order status update data is null.");

                var order = await _unitOfWork.Repository<Order>().GetAsync(statusReq.Id);
                if (order == null)
                    return null;

                order.status = statusReq.Status;

                await _unitOfWork.Repository<Order>().Update(order);
                await _unitOfWork.CompleteAsync();

                return statusReq;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while updating the order status.");
                throw new InvalidOperationException($"An error occurred while updating the status of the order with ID {statusReq.Id}.", ex);
            }
        }



        public async Task<OrderUpdateReq> UpdateOrder(OrderUpdateReq order)
        {
            try
            {
                if (order == null)
                    throw new ArgumentNullException(nameof(order), "Order update data is null.");

                var check = await _unitOfWork.Repository<Order>().GetAsync(order.Id);
                if (check == null)
                    return null;

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

                return order;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while updating a Order.");
                throw new InvalidOperationException($"An error occurred while updating the order with ID {order.Id}.", ex);
            }
        }
    }
           
}
