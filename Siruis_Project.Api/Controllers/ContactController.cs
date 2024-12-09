using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.ContactDto;
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
            try
            {
                var result = await _contactService.GetAllContacts();
                return Ok(new
                {
                    success = true,
                    message = "Contacts retrieved successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while retrieving Contacts.",
                    error = ex.Message
                });
            }
        }
        [HttpPost("AddContact")]
        public async Task<ActionResult<ContactGetReq>> AddContact(ContactAddReq contact)
        {
            try
            {
                if (contact == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Contact data provided."
                    });

                var result = await _contactService.AddContact(contact);
                if (result == null)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to add the Contact."
                    });

                return Ok(new
                {
                    success = true,
                    message = "Contact added successfully.",
                    data = result
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while adding the Contact.",
                    error = ex.Message
                });
            }

        }
        [HttpDelete("DeleteAllContacts")]
        public async Task<IActionResult> DeleteAllContacts()
        {
            try
            {
                var success = await _contactService.DeleteAllContacts();
                if (!success)
                    return BadRequest(new
                    {
                        success = false,
                        message = "No Contacts available to delete."
                    });

                return Ok(new
                {
                    success = true,
                    message = "All Contacts deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while deleting all Contacts.",
                    error = ex.Message
                });
            }
        }










    }
}
