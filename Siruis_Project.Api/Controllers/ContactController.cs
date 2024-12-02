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
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IUnitOfWork _unitOfWork;

        public ContactController(IContactService contactService,IUnitOfWork unitOfWork)
        {
            this._contactService = contactService;
            this._unitOfWork = unitOfWork;
        }
        [HttpGet("GetAllContacts")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts()
        {
            var result = await _contactService.GetAllContacts();

            return Ok(result);
        }
        [HttpPost("AddContact")]
        public async Task<ActionResult<Contact>> AddContact(Contact contact)
        {
            if (contact == null) { return BadRequest("Invalid Contact"); }
            var result = await _contactService.AddContact(contact);
            if (result == null) { return BadRequest("Can not add Contact"); }
            return Ok(contact);

        }
        [HttpDelete("DeleteAllContacts")]
        public async Task<IActionResult> DeleteAllContacts()
        {
            _contactService.DeleteAllContacts();
            _unitOfWork.CompleteAsync();
            
            return NoContent(); // Return 204 No Content to indicate successful deletion
        }










    }
}
