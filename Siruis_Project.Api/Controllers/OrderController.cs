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
            var result = await _orderServices.GetAllOrders();

            return Ok(result);
        }
        [HttpGet("GetOrderById")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var result = await _orderServices.GetOrderById(id);

            return Ok(result);
        }


        [HttpPost("AddOrder")]
        public async Task<ActionResult<Order>> AddOrder(OrderAddReq order)
        {
            if (order == null) { return BadRequest("Invalid Order"); }
            var result = await _orderServices.AddOrder(order);
            if (result == null) { return BadRequest("Can not add Order"); }
            return Ok(order);

        }
        [HttpDelete("DeleteOrderById")]
        public async Task<IActionResult> DeleteOrderById(int id)
        {
            var order = await _orderServices.GetOrderById(id);

            if (order == null)
            {
                return NotFound($"portofolio with id {id} does not exist.");
            }

            await _orderServices.DeleteOrder(id);

            await _unitOfWork.CompleteAsync(); // Save the changes

            return Ok($"Order with id {id} has been deleted successfully.");

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
        public async Task<ActionResult<Order>> UpdateOrder(OrderUpdateReq order)
        {
            if (order == null)
            {
                return BadRequest("Error");
            }
            var check = await _orderServices.GetOrderById(order.Id);
            if (check == null) { return BadRequest("Order is not Exist"); }
            var result = await _orderServices.UpdateOrder(order);

            if (result == null) { return BadRequest("Can not Update Order Data"); }

            return Ok(order);



        }
        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(OrderUpdateStatusReq  orderUpdateStatusReq)
        {
            var check = await _orderServices.GetOrderById(orderUpdateStatusReq.Id);
            if (check == null)
            {
                return BadRequest("Order is not Exist");

            }
            await _orderServices.UpdateStatus(orderUpdateStatusReq);

            return NoContent();


        }
    }

}
