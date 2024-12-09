using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.IndustryDto;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using Siruis_Project.Service.Services.Clients;
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
            try
            {
                var result = await _industryServices.GetAllIndustries();
                return Ok(new
                {
                    success = true,
                    message = "Industries retrieved successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while retrieving Industries.",
                    error = ex.Message
                });
            }
        }
        [HttpGet("GetIndustryById")]
        public async Task<ActionResult<Industry>> GetIndustryById(int id)
        {
            try
            {
                // Fetch the industry by ID using the service
                var industry = await _industryServices.GetIndustryById(id);

                // Check if the client exists
                if (industry == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = $"industry with ID {id} not found."
                    })
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                // Return the client details
                return new JsonResult(new
                {
                    success = true,
                    message = "Industry retrieved successfully.",
                    data = industry
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
                    message = "An error occurred while retrieving the Indutry.",
                    error = ex.Message
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        [HttpPost("AddIndustry")]
        public async Task<ActionResult<IndustryUpdateReq>> AddIndustry(IndustryAddReq industry)
        {
            try
            {
                if (industry == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Industry data provided."
                    });

                var result = await _industryServices.AddIndustry(industry);
                if (result == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to add the Industry."
                    });

                return Ok(new
                {
                    success = true,
                    message = "Industry added successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while adding the Industry.",
                    error = ex.Message
                });
            }

        }

        [HttpDelete("DeleteIndustryById")]
        public async Task<IActionResult> DeleteIndustryById(int id)
        {
            try
            {
                var success = await _industryServices.DeleteIndustryById(id);
                if (!success)
                    return NotFound(new
                    {
                        success = false,
                        message = $"Industry with ID {id} not found."
                    });

                return Ok(new
                {
                    success = true,
                    message = "Industry deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while deleting the Industry.",
                    error = ex.Message
                });
            }
        }



        [HttpDelete("DeleteAllIndustries")]
        public async Task<IActionResult> DeleteAllIndustries()
        {
            try
            {
                var success = await _industryServices.DeleteAllIndustries();
                if (!success)
                    return BadRequest(new
                    {
                        success = false,
                        message = "No Industries available to delete."
                    });

                return Ok(new
                {
                    success = true,
                    message = "All industries deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while deleting all industries.",
                    error = ex.Message
                });
            }
        }
        [HttpPost("UpdateIndustry")]
        public async Task<ActionResult<IndustryUpdateReq>> UpdateIndustry(IndustryUpdateReq industry)
        {
            try
            {
                if (industry == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid industry data provided."
                    });

                var result = await _industryServices.UpdateIndustry(industry);
                if (result == null)
                    return NotFound(new
                    {
                        success = false,
                        message = $"Industry with ID {industry.Id} not found."
                    });

                return Ok(new
                {
                    success = true,
                    message = "Industry updated successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while updating the Industry.",
                    error = ex.Message
                });
            }



        }






    }
}
