using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.ClientDto;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Service.Services.Clients
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ClientUpdateReq> AddCLient(ClientAddReq client)
        {
            if (client == null) return null;

            var request = new Client
            {
                Name = client.Name,
                PictureUrl = client.PictureUrl,

            };
            var response = new ClientUpdateReq
            {
                Id = request.Id,
                Name = request.Name,
                PictureUrl = request.PictureUrl,

            };
            await _unitOfWork.Repository<Client>().AddAsync(request);
            await _unitOfWork.CompleteAsync();
            return response;
        }

        public async Task<bool> DeleteAllClient()
        {
            try
            {
                // Fetch all Clients
                var clients = await _unitOfWork.Repository<Client>().GetAllAsync();

                // Check if there are no Clients to delete
                if (clients == null || !clients.Any())
                    return false;

                // Remove all Clients
                _unitOfWork.Repository<Client>().DeleteAll();
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch
            {

                return false;
            }

        }



        public async Task<bool> DeleteCLientById(int id)
        {
            try
            {
                // Check if UnitOfWork is initialized
                if (_unitOfWork == null)
                    throw new InvalidOperationException("Unit of Work is not initialized.");

                // Get the repository for Client
                var clientRepository = _unitOfWork.Repository<Client>();
                if (clientRepository == null)
                    throw new InvalidOperationException("Client repository is not initialized.");

                // Fetch the Client by id
                var client = await clientRepository.GetAsync(id);
                if (client == null)
                    return false; // Client with the given id does not exist

                // Delete the Client
                clientRepository.Delete(client);

                // Commit the changes
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if necessary (e.g., using a logging framework)
                // Logger.LogError(ex, "An error occurred while deleting the client.");

                return false; // Return false to indicate failure
            }
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of Work is not initialized.");
            }

            var clientRepository = _unitOfWork.Repository<Client>();
            if (clientRepository == null)
            {
                throw new InvalidOperationException("Client repository is not initialized.");
            }

            var result = await clientRepository.GetAllAsync();
            return result ?? Enumerable.Empty<Client>();
        }


        public async Task<Client?> GetClientById(int id)
        {
            var client =await  _unitOfWork.Repository<Client>().GetAsync(id);
            if (client == null) return null;
            return client;
            
        }

        public async Task<ClientUpdateReq> UpdateClient(ClientUpdateReq client)
        {
            var check = await _unitOfWork.Repository<Client>().GetAsync(client.Id);
            if (check is null) return null;

            // Update properties explicitly
            check.Name = client.Name;
            check.PictureUrl = client.PictureUrl;

           await _unitOfWork.Repository<Client>().Update(check);
            await _unitOfWork.CompleteAsync();
            return client; // Return the updated entity
        }


    }
}
