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

        Task<IEnumerable< Contact>> GetAllContacts();

       

        Task<Contact> AddContact(Contact contact);

        void DeleteAllContacts();

    }
}
