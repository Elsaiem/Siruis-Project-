using Siruis_Project.Core.Dtos.ClientDto;
using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.ServiceContract
{
    public interface IClientService
    {
        Task<IEnumerable<ClientUpdateReq>> GetAllClients();

        Task<ClientUpdateReq?> GetClientById(int id);

        Task<ClientUpdateReq> AddClient(ClientAddReq client);

        Task<bool> DeleteClientById(int id);

        Task<ClientUpdateReq> UpdateClient(ClientUpdateReq client);

        Task<bool> DeleteAllClients();


    }
}
