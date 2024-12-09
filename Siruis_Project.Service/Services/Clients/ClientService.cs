using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.ClientDto;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siruis_Project.Service.Services.Clients
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ClientUpdateReq> AddClient(ClientAddReq client)
        {
            try
            {
                if (client == null)
                    throw new ArgumentNullException(nameof(client), "Client data is null.");

                var newClient = new Client
                {
                    Name = client.Name,
                    PictureUrl = client.PictureUrl,
                };

                await _unitOfWork.Repository<Client>().AddAsync(newClient);
                await _unitOfWork.CompleteAsync();

                return new ClientUpdateReq
                {
                    Id = newClient.Id,
                    Name = newClient.Name,
                    PictureUrl = newClient.PictureUrl,
                };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while adding a client.");
                throw new InvalidOperationException("An error occurred while adding the client.", ex);
            }
        }

        public async Task<bool> DeleteAllClients()
        {
            try
            {
                var clients = await _unitOfWork.Repository<Client>().GetAllAsync();
                if (clients == null || !clients.Any())
                    return false;

                _unitOfWork.Repository<Client>().DeleteAll();
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while deleting all clients.");
                return false;
            }
        }

        public async Task<bool> DeleteClientById(int id)
        {
            try
            {
                var clientRepository = _unitOfWork.Repository<Client>();
                var client = await clientRepository.GetAsync(id);
                if (client == null)
                    return false;

                clientRepository.Delete(client);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while deleting a client by ID.");
                return false;
            }
        }

        public async Task<IEnumerable<ClientUpdateReq>> GetAllClients()
        {
            try
            {
                var clientRepository = _unitOfWork.Repository<Client>();
                var clients = await clientRepository.GetAllAsync();
                if (clients == null || !clients.Any())
                    return Enumerable.Empty<ClientUpdateReq>();

                var response = clients.Select(client => new ClientUpdateReq
                {
                    Id = client.Id,
                    Name = client.Name,
                    PictureUrl = client.PictureUrl
                });

                return response;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while retrieving all clients.");
                throw new InvalidOperationException("An error occurred while retrieving clients.", ex);
            }
        }


        public async Task<ClientUpdateReq?> GetClientById(int id)
        {
            try
            {
                var client = await _unitOfWork.Repository<Client>().GetAsync(id);
                if (client is null) return null;
                var response = new ClientUpdateReq {
                 Id = client.Id,
                 Name = client.Name,
                 PictureUrl = client.PictureUrl,
                
                
                };

                return response ?? null;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while retrieving a client by ID.");
                throw new InvalidOperationException($"An error occurred while retrieving the client with ID {id}.", ex);
            }
        }

        public async Task<ClientUpdateReq?> UpdateClient(ClientUpdateReq client)
        {
            try
            {
                if (client == null)
                    throw new ArgumentNullException(nameof(client), "Client update data is null.");

                var existingClient = await _unitOfWork.Repository<Client>().GetAsync(client.Id);
                if (existingClient == null)
                    return null;

                existingClient.Name = client.Name;
                existingClient.PictureUrl = client.PictureUrl;

                await _unitOfWork.Repository<Client>().Update(existingClient);
                await _unitOfWork.CompleteAsync();

                return client;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while updating a client.");
                throw new InvalidOperationException($"An error occurred while updating the client with ID {client.Id}.", ex);
            }
        }
    }
}
