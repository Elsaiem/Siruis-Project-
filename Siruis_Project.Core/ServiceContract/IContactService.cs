using Siruis_Project.Core.Dtos.ContactDto;
using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.ServiceContract
{
    public interface IContactService
    {

        Task<IEnumerable<Contact>> GetAllContacts();

       

        Task<ContactGetReq> AddContact(ContactAddReq contact);

        Task<bool> DeleteAllContacts();

    }
}
