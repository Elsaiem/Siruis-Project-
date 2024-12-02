using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;

namespace Siruis_Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        private readonly IUnitOfWork _UnitOfWork;

        public ClientController(IClientService clientService,IUnitOfWork unitOfWork)
        {
            this._clientService = clientService;
            _UnitOfWork = unitOfWork;
        }

        [HttpGet("GetAllClients")]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            var result = await _clientService.GetAllClients();

            return Ok(result);
        }

        [HttpPost("AddClient")]
        public async Task<ActionResult<Client>> AddClient(Client client)
        {
            if (client == null) { return BadRequest("Invalid client"); }
            var result=await _clientService.AddCLient(client);
            if (result == null) { return BadRequest("Can not add Client"); }
            return Ok(client);

        }
        [HttpDelete("DeleteClient")]
        public async Task DeleteClient(Client client)
        {
            _clientService.DeleteCLient(client);
            await _UnitOfWork.CompleteAsync();

        }

        [HttpDelete("DeleteAllClients")]
        public async Task<IActionResult> DeleteAllClients()
        {
            _clientService.DeleteAllClient();
            await _UnitOfWork.CompleteAsync(); // Commit changes asynchronously
            return NoContent(); // Return 204 No Content to indicate successful deletion
        }

        [HttpPost("UpdateClient")]
        public async Task<ActionResult<Client>> UpdateCLient(Client client)
        {
            if (client == null)
            {
                return BadRequest("Error");
            }
            var result= await _clientService.UpdateClient(client);
            if (result == null) { return BadRequest("Can not Update Client Data"); }

            return Ok(client);
        
        
        
        }


    }
}
