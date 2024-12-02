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

        [HttpGet("CountAllOrders")]
        public async Task<IActionResult> CountOrders()
        {
            var count = await _unitOfWork.Repository<Order>().CountEntity();
            return Ok(new { Count = count }); // Returning count as a JSON object
        }



        //Count CLients
        [HttpGet("CountAllClients")]
        public async Task<IActionResult> CountAllClients()
        {
            var count = await _unitOfWork.Repository<Client>().CountEntity();
            return Ok(new { Count = count }); // Returning count as a JSON object
        }
        //Count Portofolio
        [HttpGet("CountAllPortotfolios")]
        public async Task<IActionResult> CountAllPortotfolios()
        {
            var count = await _unitOfWork.Repository<Portofolio>().CountEntity();
            return Ok(new { Count = count }); // Returning count as a JSON object
        }




    }
}
