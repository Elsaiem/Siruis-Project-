using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.OrderDto;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using Siruis_Project.Service.Services.Clients;
using Siruis_Project.Service.Services.Portofolios;
using Siruis_Project.Service.Services.Tasks;

namespace Siruis_Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderServices _orderServices;

        public OrderController(IUnitOfWork unitOfWork, IOrderServices orderServices)
        {
            _unitOfWork = unitOfWork;
            _orderServices = orderServices;
        }

        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrder()
        {
            try
            {
                var result = await _orderServices.GetAllOrders();
                return Ok(new
                {
                    success = true,
                    message = "Orders retrieved successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while retrieving Orders.",
                    error = ex.Message
                });
            }
        }
        [HttpGet("GetOrderById")]
        public async Task<ActionResult> GetOrderById(int id)
        {
            try
            {
                // Fetch the Order by ID using the service
                var order = await _orderServices.GetOrderById(id);

                // Check if the order exists
                if (order == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = $"Order with ID {id} not found."
                    })
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                // Return the client details
                return new JsonResult(new
                {
                    success = true,
                    message = "Order retrieved successfully.",
                    data = order
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return new JsonResult(new
                {
                    success = false,
                    message = "An error occurred while retrieving the order.",
                    error = ex.Message
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


        [HttpPost("AddOrder")]
        public async Task<ActionResult> AddOrder(OrderAddReq order)
        {
            try
            {
                if (order == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid order data provided."
                    });

                var result = await _orderServices.AddOrder(order);
                if (result == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to add the order."
                    });

                return Ok(new
                {
                    success = true,
                    message = "order added successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while adding the order.",
                    error = ex.Message
                });
            }

        }
        [HttpDelete("DeleteOrderById")]
        public async Task<IActionResult> DeleteOrderById(int id)
        {
            try
            {
                var success = await _orderServices.DeleteOrder(id);
                if (!success)
                    return NotFound(new
                    {
                        success = false,
                        message = $"order with ID {id} not found."
                    });

                return Ok(new
                {
                    success = true,
                    message = "order deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while deleting the order.",
                    error = ex.Message
                });
            }

        }

        [HttpDelete("DeleteAllOrders")]
        public async Task<IActionResult> DeleteAllOrders()
        {
            try
            {
                var result = await _orderServices.DeleteAllOrders(); // Assuming it returns a Task<bool>

                if (result) // Check if deletion was successful
                {
                    return new JsonResult(new { success = true, message = "All orders deleted successfully." })
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                else
                {
                    return new JsonResult(new { success = false, message = "No orders were deleted. Please try again." })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = "An error occurred while deleting orders.", details = ex.Message })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpPost("UpdateOrder")]
        public async Task<ActionResult> UpdateOrder(OrderUpdateReq order)
        {
            try
            {
                if (order == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid order data provided."
                    });

                var result = await _orderServices.UpdateOrder(order);
                if (result == null)
                    return NotFound(new
                    {
                        success = false,
                        message = $"Order with ID {order.Id} not found."
                    });

                return Ok(new
                {
                    success = true,
                    message = "order updated successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while updating the order.",
                    error = ex.Message
                });
            }



        }
        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(OrderUpdateStatusReq orderUpdateStatusReq)
        {
            try
            {
                if (orderUpdateStatusReq == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid order status update data provided."
                    });

                var check = await _orderServices.GetOrderById(orderUpdateStatusReq.Id);
                if (check == null)
                    return NotFound(new
                    {
                        success = false,
                        message = $"Order with ID {orderUpdateStatusReq.Id} not found."
                    });

                await _orderServices.UpdateStatus(orderUpdateStatusReq);

                return Ok(new
                {
                    success = true,
                    message = "Order status updated successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while updating the order status.",
                    error = ex.Message
                });
            }
        }

    }

}
