using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.TeamMemberDto;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using Siruis_Project.Service.Services.Clients;

namespace Siruis_Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberService _teamMemberService;
        private readonly IUnitOfWork _unitOfWork;

        public TeamMemberController(ITeamMemberService teamMemberService,IUnitOfWork unitOfWork)
        {
            this._teamMemberService = teamMemberService;
            this._unitOfWork = unitOfWork;
        }
        [HttpGet("GetAllTeamMembers")]
        public async Task<ActionResult<IEnumerable<TeamMember>>> GetAllTeamMembers()
        {
            try
            {
                var result = await _teamMemberService.GetAllMembers();
                return Ok(new
                {
                    success = true,
                    message = "Members retrieved successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while retrieving Members.",
                    error = ex.Message
                });
            }
        }
        [HttpGet("GetTeamMember")]
        public async Task<ActionResult<TeamMember>> GetTeamMember( int id)
        {
            try
            {
                // Fetch the member by ID using the service
                var member = await _teamMemberService.GetMemberById(id);

                // Check if the member exists
                if (member == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = $"Member with ID {id} not found."
                    })
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                // Return the member details
                return new JsonResult(new
                {
                    success = true,
                    message = "member retrieved successfully.",
                    data = member
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
                    message = "An error occurred while retrieving the member.",
                    error = ex.Message
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        [HttpPost("AddMember")]
        public async Task<ActionResult<TeamMemberUpdateReq>> AddMember(TeamMemberAddReq member)
        {
            try
            {
                if (member == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Member data provided."
                    });

                var result = await _teamMemberService.AddMember(member);
                if (result == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to add the member."
                    });

                return Ok(new
                {
                    success = true,
                    message = "Member added successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while adding the member.",
                    error = ex.Message
                });
            }

        }

        [HttpDelete("DeleteAllTeamMember")]
        public async Task<IActionResult> DeleteAllTeamMember()
        {
            try
            {
                var success = await _teamMemberService.DeleteAllTeamMembers();
                if (!success)
                    return BadRequest(new
                    {
                        success = false,
                        message = "No Members available to delete."
                    });

                return Ok(new
                {
                    success = true,
                    message = "All Members deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while deleting all Members.",
                    error = ex.Message
                });
            }
        }
        [HttpDelete("DeleteTeammember")]
        public async Task<IActionResult> DeleteTeammember(int id)
        {
            try
            {
                var success = await _teamMemberService.DeleteMember(id);
                if (!success)
                    return NotFound(new
                    {
                        success = false,
                        message = $"Memebr with ID {id} not found."
                    });

                return Ok(new
                {
                    success = true,
                    message = "Member deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while deleting the Member.",
                    error = ex.Message
                });
            }
        }

        [HttpPost("UpdateTeamMember")]
        public async Task<ActionResult<TeamMemberUpdateReq>> UpdateTeamMember(TeamMemberUpdateReq member)
        {
            try
            {
                if (member == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Member data provided."
                    });

                var result = await _teamMemberService.UpdateMember(member);
                if (result == null)
                    return NotFound(new
                    {
                        success = false,
                        message = $"member with ID {member.Id} not found."
                    });

                return Ok(new
                {
                    success = true,
                    message = "member updated successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while updating the Member.",
                    error = ex.Message
                });
            }



        }





    }
}
