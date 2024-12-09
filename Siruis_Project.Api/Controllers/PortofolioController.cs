using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.PortofolioD;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using Siruis_Project.Service.Services.Clients;
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
            try
            {
                var result = await _portofolioServices.GetAllPortofolios();
                return Ok(new
                {
                    success = true,
                    message = "Portofolios retrieved successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while retrieving portofolios.",
                    error = ex.Message
                });
            }
        }
        [HttpGet("GetPortofolioById")]
        public async Task<ActionResult<Portofolio>> GetPortofolioById(int id)
        {
            try
            {
                // Fetch the Portofolio by ID using the service
                var portofolio = await _portofolioServices.GetPortofolioById(id);

                // Check if the Portofolio exists
                if (portofolio == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = $"portofolio with ID {id} not found."
                    })
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                // Return the Portofolio details
                return new JsonResult(new
                {
                    success = true,
                    message = "portofolio retrieved successfully.",
                    data = portofolio
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
                    message = "An error occurred while retrieving the portofolio.",
                    error = ex.Message
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        [HttpPost("AddPortofolio")]
        public async Task<ActionResult<PortofolioUpdateReq>> AddPortofolio(PorotofolioAddReq portofolio)
        {
            try
            {
                if (portofolio == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid portofolio data provided."
                    });

                var result = await _portofolioServices.AddPortofio(portofolio);
                if (result == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to add the portofolio."
                    });

                return Ok(new
                {
                    success = true,
                    message = "portofolio added successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while adding the portofolio.",
                    error = ex.Message
                });
            }

        }

        [HttpDelete("DeletePortofolioById")]
        public async Task<IActionResult> DeletePortofolioById(int id)
        {
            try
            {
                var success = await _portofolioServices.DeletePortofolioById(id);
                if (!success)
                    return NotFound(new
                    {
                        success = false,
                        message = $"portofolio with ID {id} not found."
                    });

                return Ok(new
                {
                    success = true,
                    message = "portofolio deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while deleting the portofolio.",
                    error = ex.Message
                });
            }
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
        public async Task<ActionResult<PortofolioUpdateReq>> UpdatePortofolio(PortofolioUpdateReq portofolio)
        {
            try
            {
                if (portofolio == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid portofolio data provided."
                    });

                var result = await _portofolioServices.UpdatePortofolio(portofolio);
                if (result == null)
                    return NotFound(new
                    {
                        success = false,
                        message = $"portofolio with ID {portofolio.Id} not found."
                    });

                return Ok(new
                {
                    success = true,
                    message = "portofolio updated successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while updating the portofolio.",
                    error = ex.Message
                });
            }



        }


    }
}
