using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core.Dtos.ClientDto;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;

namespace Siruis_Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        }

        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var result = await _clientService.GetAllClients();
                return Ok(new
                {
                    success = true,
                    message = "Clients retrieved successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while retrieving clients.",
                    error = ex.Message
                });
            }
        }
        [HttpGet("GetClientById")]
        public async Task<IActionResult> GetClientById(int id)
        {
            try
            {
                // Fetch the client by ID using the service
                var client = await _clientService.GetClientById(id);

                // Check if the client exists
                if (client == null) 
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = $"Client with ID {id} not found."
                    })
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                // Return the client details
                return new JsonResult(new
                {
                    success = true,
                    message = "Client retrieved successfully.",
                    data = client
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
                    message = "An error occurred while retrieving the client.",
                    error = ex.Message
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }





        [HttpPost("AddClient")]
        public async Task<IActionResult> AddClient([FromBody] ClientAddReq client)
        {
            try
            {
                if (client == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid client data provided."
                    });

                var result = await _clientService.AddClient(client);
                if (result == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to add the client."
                    });

                return Ok(new
                {
                    success = true,
                    message = "Client added successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while adding the client.",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("DeleteClientById")]
        public async Task<IActionResult> DeleteClientById(int id)
        {
            try
            {
                var success = await _clientService.DeleteClientById(id);
                if (!success)
                    return NotFound(new
                    {
                        success = false,
                        message = $"Client with ID {id} not found."
                    });

                return Ok(new
                {
                    success = true,
                    message = "Client deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while deleting the client.",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("DeleteAllClients")]
        public async Task<IActionResult> DeleteAllClients()
        {
            try
            {
                var success = await _clientService.DeleteAllClients();
                if (!success)
                    return BadRequest(new
                    {
                        success = false,
                        message = "No clients available to delete."
                    });

                return Ok(new
                {
                    success = true,
                    message = "All clients deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while deleting all clients.",
                    error = ex.Message
                });
            }
        }

        [HttpPost("UpdateClient")]
        public async Task<IActionResult> UpdateClient([FromBody] ClientUpdateReq client)
        {
            try
            {
                if (client == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid client data provided."
                    });

                var result = await _clientService.UpdateClient(client);
                if (result == null)
                    return NotFound(new
                    {
                        success = false,
                        message = $"Client with ID {client.Id} not found."
                    });

                return Ok(new
                {
                    success = true,
                    message = "Client updated successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while updating the client.",
                    error = ex.Message
                });
            }
        }
    }
}
