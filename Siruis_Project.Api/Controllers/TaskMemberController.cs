using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using Siruis_Project.Service.Services.TeamMembers;

namespace Siruis_Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskMemberController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITaskService _taskService;

        public TaskMemberController(IUnitOfWork unitOfWork,ITaskService taskService)
        {
            this._unitOfWork = unitOfWork;
            this._taskService = taskService;
        }

        [HttpGet("GetAllTasks")]
        public async Task<ActionResult<TaskMember>> GetAllTasks() {

            var result = await _taskService.GetAllTasks();

            return Ok(result);

        }
        [HttpPost("AddTask")]
        public async Task<ActionResult<TaskMember>> AddTask(TaskMember task)
        {
            if (task == null) { return BadRequest("Invalid task"); }
            var result = await _taskService.AddTask(task);
            if (result == null) { return BadRequest("Can not add task"); }
            return Ok(task);

        }

        [HttpGet("GetTaskById")]
        public async Task<ActionResult<TaskMember>> GetTaskById(int id)
        {
            var result = await _taskService.GetTaskById(id);

            return Ok(result);
        }

        [HttpPost("UpdateTask")]
        public async Task<ActionResult<TaskMember>> UpdateTask(TaskMember task)
        {
            if (task == null)
            {
                return BadRequest("Error");
            }
            var check = await _taskService.GetTaskById(task.Id);
            if (check == null) { return BadRequest("Task is not Exist"); }
            var result =await _taskService.UpdateTask(task);
            
            if (result == null) { return BadRequest("Can not Update Task Data"); }

            return Ok(task);



        }
        [HttpDelete("DeleteAllTasks")]
        public async Task<IActionResult> DeleteAllTasks()
        {
            await _taskService.DeleteAllTasks();
            // Commit changes asynchronously
            return NoContent(); // Return 204 No Content to indicate successful deletion
        }
        [HttpDelete("DeleteTaskById")]
        public async Task DeleteTaskById(int id)
        {
            await _taskService.DeleteTask(id);
        }







    }
}
