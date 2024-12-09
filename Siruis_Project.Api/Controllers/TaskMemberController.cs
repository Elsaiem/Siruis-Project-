using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.TaskDto;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using Siruis_Project.Service.Services.Clients;
using Siruis_Project.Service.Services.Contacts;
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

            try
            {
                var result = await _taskService.GetAllTasks();
                return Ok(new
                {
                    success = true,
                    message = "Tasks retrieved successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while retrieving Tasks.",
                    error = ex.Message
                });
            }

        }
        [HttpGet("GetTaskById")]
        public async Task<ActionResult<TaskUpdateReq>> GetTaskById(int id)
        {
            try
            {
                // Fetch the client by ID using the service
                var task = await _taskService.GetTaskById(id);

                // Check if the client exists
                if (task == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = $"task with ID {id} not found."
                    })
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                // Return the client details
                return new JsonResult(new
                {
                    success = true,
                    message = "Task retrieved successfully.",
                    data = task 
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
                    message = "An error occurred while retrieving the Task.",
                    error = ex.Message
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        [HttpPost("AddTask")]
        public async Task<ActionResult<TaskUpdateReq>> AddTask(TaskAddReq task)
        {
            try
            {
                if (task == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid client data provided."
                    });

                var result = await _taskService.AddTask(task);
                if (result == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to add the Task."
                    });

                return Ok(new
                {
                    success = true,
                    message = "Task added successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while adding the Task.",
                    error = ex.Message
                });
            }

        }

      

        [HttpPost("UpdateTask")]
        public async Task<ActionResult<TaskUpdateReq>> UpdateTask(TaskUpdateReq task)
        {
            try
            {
                if (task == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Task data provided."
                    });

                var result = await _taskService.UpdateTask(task);
                if (result == null)
                    return NotFound(new
                    {
                        success = false,
                        message = $"Task with ID {task.Id} not found."
                    });

                return Ok(new
                {
                    success = true,
                    message = "Task updated successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while updating the Task.",
                    error = ex.Message
                });
            }

        }
        [HttpDelete("DeleteAllTasks")]
        public async Task<IActionResult> DeleteAllTasks()
        {
            try
            {
                var success = await _taskService.DeleteAllTasks();
                if (!success)
                    return BadRequest(new
                    {
                        success = false,
                        message = "No Tasks available to delete."
                    });

                return Ok(new
                {
                    success = true,
                    message = "All Tasks deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while deleting all Tasks.",
                    error = ex.Message
                });
            }
        }
        [HttpDelete("DeleteTaskById")]
        public async Task<IActionResult> DeleteTaskById(int id)
        {
            try
            {
                var success = await _taskService.DeleteTask(id);
                if (!success)
                    return NotFound(new
                    {
                        success = false,
                        message = $"Task with ID {id} not found."
                    });

                return Ok(new
                {
                    success = true,
                    message = "Task deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while deleting the Task.",
                    error = ex.Message
                });
            }
        }







    }
}
