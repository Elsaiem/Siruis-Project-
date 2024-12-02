using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
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
            var result = await _teamMemberService.GetAllMembers();

            return Ok(result);
        }
        [HttpGet("GetTeamMember")]
        public async Task<ActionResult<TeamMember>> GetTeamMember( int id)
        {
            var result = await _teamMemberService.GetMemberById(id);

            return Ok(result);
        }
        [HttpPost("AddMember")]
        public async Task<ActionResult<TeamMember>> AddMember(TeamMember member)
        {
            if (member == null) { return BadRequest("Invalid member"); }
            var result = await _teamMemberService.AddMember(member);
            if (result == null) { return BadRequest("Can not add member"); }
            return Ok(member);

        }

        [HttpDelete("DeleteAllTeamMember")]
        public async Task<IActionResult> DeleteAllTeamMember()
        {
           await _teamMemberService.DeleteAllTeamMembers();
              // Commit changes asynchronously
            return NoContent(); // Return 204 No Content to indicate successful deletion
        }
        [HttpDelete("DeleteTeammember")]
        public async Task DeleteTeammember(int id)
        {
            await _teamMemberService.DeleteMember(id);
        }

        [HttpPost("UpdateTeamMember")]
        public async Task<ActionResult<TeamMember>> UpdateTeamMember(TeamMember member)
        {
            if (member == null)
            {
                return BadRequest("Error");
            }
            var check =await _teamMemberService.GetMemberById(member.Id);
            if (check == null) { return BadRequest("Member is not Exist"); }
            var result =await _teamMemberService.UpdateMember(member);
          
            if (result == null) { return BadRequest("Can not Update Member Data"); }

            return Ok(member);



        }





    }
}
