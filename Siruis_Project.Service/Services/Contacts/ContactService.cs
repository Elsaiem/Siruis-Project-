using Siruis_Project.Core;
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
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of Work is not initialized.");
            }

            var clientRepository = _unitOfWork.Repository<Contact>();
            if (clientRepository == null)
            {
                throw new InvalidOperationException("Client repository is not initialized.");
            }

            var result = await clientRepository.GetAllAsync();
            return result ?? Enumerable.Empty<Contact>();
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            if (contact == null) return null;


            await _unitOfWork.Repository<Contact>().AddAsync(contact);
            await _unitOfWork.CompleteAsync();
            return contact;
        }

        public void DeleteAllContacts()
        {
              _unitOfWork.Repository<Contact>().DeleteAll();
             
        }

      
    }
}
