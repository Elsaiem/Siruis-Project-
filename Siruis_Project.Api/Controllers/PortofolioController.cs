using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.PortofolioD;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using Siruis_Project.Service.Services.Industries;

namespace Siruis_Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortofolioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPortofolioServices _portofolioServices;

        public PortofolioController(IUnitOfWork unitOfWork,IPortofolioServices portofolioServices)
        {
            _unitOfWork = unitOfWork;
            _portofolioServices = portofolioServices;
        }

        [HttpGet("GetAllPortofolios")]
        public async Task<ActionResult<IEnumerable<Portofolio>>> GetAllPortofolios()
        {
            var result = await _portofolioServices.GetAllPortofolios();

            return Ok(result);
        }
        [HttpGet("GetPortofolioById")]
        public async Task<ActionResult<Portofolio>> GetPortofolioById(int id)
        {
            var result = await _portofolioServices.GetPortofolioById(id);
            if (result is null) return BadRequest("Porotofolio is Not Found");
            return Ok(result);
        }
        [HttpPost("AddPortofolio")]
        public async Task<ActionResult<Portofolio>> AddPortofolio(PorotofolioAddReq portofolio)
        {
            if (portofolio == null) { return BadRequest("Invalid Portofolio"); }
            var result = await _portofolioServices.AddPortofio(portofolio);
            if (result == null) { return BadRequest("Can not add portofolio"); }
            return Ok(portofolio);

        }

        [HttpDelete("DeletePortofolioById")]
        public async Task<IActionResult> DeletePortofolioById(int id)
        {

            var portofolio = await _portofolioServices.GetPortofolioById(id);

            if (portofolio == null)
            {
                return NotFound($"portofolio with id {id} does not exist.");
            }

            await _portofolioServices.DeletePortofolioById(id);

            await _unitOfWork.CompleteAsync(); // Save the changes

            return Ok($"portofolio with id {id} has been deleted successfully.");
        }
        [HttpDelete("DeleteAllPortofolios")]
                public async Task<IActionResult> DeleteAllPortofolios()
        {
            try
            {
                var result = await _portofolioServices.DeleteAllPortofolio();

                if (result) // Assuming `DeleteAllPortofolio` returns true if deletion is successful
                {
                    return new JsonResult(new { success = true, message = "All portfolios deleted successfully." })
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                else
                {
                    return new JsonResult(new { success = false, message = "No portfolios were deleted. Please try again." })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = "An error occurred while deleting portfolios.", details = ex.Message })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpPost("UpdatePortofolio")]
        public async Task<ActionResult<Portofolio>> UpdatePortofolio(PortofolioUpdateReq portofolio)
        {
            if (portofolio == null)
            {
                return BadRequest("Error");
            }
            var check = await _portofolioServices.GetPortofolioById(portofolio.Id);
            if (check == null) { return BadRequest("Portofolio is not Exist"); }
            var result = await _portofolioServices.UpdatePortofolio(portofolio);

            if (result == null) { return BadRequest("Can not Update Portofolio Data"); }

            return Ok(portofolio);



        }


    }
}
