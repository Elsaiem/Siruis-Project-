using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using Siruis_Project.Service.Services.Orders;

namespace Siruis_Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIndustryServices _industryServices;

        public IndustryController(IUnitOfWork unitOfWork,IIndustryServices   industryServices)
        {
            _unitOfWork = unitOfWork;
            _industryServices = industryServices;
        }
        [HttpGet("GetAllIndustries")]
        public async Task<ActionResult<IEnumerable<Industry>>> GetAllIndustries()
        {
            var result = await _industryServices.GetAllIndustries();

            return Ok(result);
        }
        [HttpGet("GetIndustryById{id}")]
        public async Task<ActionResult<Industry>> GetIndustryById(int id)
        {
            var result = await _industryServices.GetIndustryById(id);
            if (result is null) return BadRequest("Industry Not Found");
            return Ok(result);
        }
        [HttpPost("AddIndustry")]
        public async Task<ActionResult<Industry>> AddIndustry(Industry industry)
        {
            if (industry == null) { return BadRequest("Invalid Industry"); }
            var result = await _industryServices.AddIndustry(industry);
            if (result == null) { return BadRequest("Can not add Industry"); }
            return Ok(industry);

        }

        [HttpDelete("DeleteIndustryById{id}")]
        public async Task<IActionResult> DeleteIndustryById(int id)
        {

            var industry = await _industryServices.GetIndustryById(id);

            if (industry == null)
            {
                return NotFound($"Industry with id {id} does not exist.");
            }

            await _industryServices.DeleteIndustryById(id);

            await _unitOfWork.CompleteAsync(); // Save the changes

            return Ok($"Industry with id {id} has been deleted successfully.");
        }



        [HttpDelete("DeleteAllIndustries")]
        public async Task<IActionResult> DeleteAllIndustries()
        {
            await _industryServices.DeleteAllIndustries();
            // Commit changes asynchronously
            return NoContent(); // Return 204 No Content to indicate successful deletion
        }
        [HttpPost("UpdateIndustry")]
        public async Task<ActionResult<Industry>> UpdateIndustry(Industry industry)
        {
            if (industry == null)
            {
                return BadRequest("Error");
            }
            var check = await _industryServices.GetIndustryById(industry.Id);
            if (check == null) { return BadRequest("Industry is not Exist"); }
            var result = await _industryServices.UpdateIndustry(industry);

            if (result == null) { return BadRequest("Can not Update Industry Data"); }

            return Ok(industry);



        }






    }
}
