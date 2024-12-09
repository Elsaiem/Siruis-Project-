using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.ClientDto;
using Siruis_Project.Core.Dtos.ContactDto;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Service.Services.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            try
            {
                var contactRepository = _unitOfWork.Repository<Contact>();
                var result = await contactRepository.GetAllAsync();
                return result ?? Enumerable.Empty<Contact>();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while retrieving all clients.");
                throw new InvalidOperationException("An error occurred while retrieving Contact.", ex);
            }
        }

        public async Task<ContactGetReq> AddContact(ContactAddReq contact)
        {
            try
            {
                if (contact == null)
                    throw new ArgumentNullException(nameof(contact), "Client data is null.");

                var newcontact = new Contact
                {
                    Name = contact.Name,
                    Email = contact.Email,
                    Subject = contact.Subject,
                    Message = contact.Message,
                };

                await _unitOfWork.Repository<Contact>().AddAsync(newcontact);
                await _unitOfWork.CompleteAsync();

                return new ContactGetReq
                {
                    Id = newcontact.Id,
                    Name = contact.Name,
                    Email = contact.Email,
                    Subject = contact.Subject,
                    Message = contact.Message,
                };
            } 
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while adding a Contact.");
                throw new InvalidOperationException("An error occurred while adding the Contact.", ex);
    }

}

        public async Task<bool> DeleteAllContacts()
        {
            try
            {
                var contacts = await _unitOfWork.Repository<Contact>().GetAllAsync();
                if (contacts == null || !contacts.Any())
                    return false;

                _unitOfWork.Repository<Contact>().DeleteAll();
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while deleting all Contacts.");
                return false;
            }

        }

      
    }
}
