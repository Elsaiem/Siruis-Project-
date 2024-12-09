using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Entities;

namespace Siruis_Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;



       
        public DashBoardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //count Orders

        // Count Orders
        [HttpGet("CountAllOrders")]
        public async Task<IActionResult> CountOrders()
        {
            try
            {
                var count = await _unitOfWork.Repository<Order>().CountEntity();
                return Ok(new
                {
                    success = true,
                    message = "Order count retrieved successfully.",
                    data = new { Count = count }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while retrieving the order count.",
                    error = ex.Message
                });
            }
        }

        // Count Clients
        [HttpGet("CountAllClients")]
        public async Task<IActionResult> CountAllClients()
        {
            try
            {
                var count = await _unitOfWork.Repository<Client>().CountEntity();
                return Ok(new
                {
                    success = true,
                    message = "Client count retrieved successfully.",
                    data = new { Count = count }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while retrieving the client count.",
                    error = ex.Message
                });
            }
        }

        // Count Portfolios
        [HttpGet("CountAllPortfolios")]
        public async Task<IActionResult> CountAllPortfolios()
        {
            try
            {
                var count = await _unitOfWork.Repository<Portofolio>().CountEntity();
                return Ok(new
                {
                    success = true,
                    message = "Portfolio count retrieved successfully.",
                    data = new { Count = count }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while retrieving the portfolio count.",
                    error = ex.Message
                });
            }
        }

        // Count All Entities
        [HttpGet("CountAll")]
        public async Task<IActionResult> CountAll()
        {
            try
            {
                var countClient = await _unitOfWork.Repository<Client>().CountEntity();
                var countPortfolio = await _unitOfWork.Repository<Portofolio>().CountEntity();
                var countOrder = await _unitOfWork.Repository<Order>().CountEntity();

                return Ok(new
                {
                    success = true,
                    message = "Counts retrieved successfully.",
                    data = new
                    {
                        CountClient = countClient,
                        CountPortfolio = countPortfolio,
                        CountOrder = countOrder
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while retrieving the counts.",
                    error = ex.Message
                });
            }
        }




    }
}
