using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.ClientDto;
using Siruis_Project.Core.Dtos.PortofolioD;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Service.Services.Portofolios
{
    public class PortofolioServices : IPortofolioServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public PortofolioServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<PortofolioUpdateReq>> GetAllPortofolios()
        {
            try
            {
                var portofolioRepository = _unitOfWork.Repository<Portofolio>();
                var portofolios = await portofolioRepository.GetAllAsync();
                if (portofolios == null || !portofolios.Any())
                    return Enumerable.Empty<PortofolioUpdateReq>();

                var response = portofolios.Select(portofolio => new PortofolioUpdateReq
                {
                    Id = portofolio.Id,
                    CLient_Id = portofolio.CLient_Id,
                    Img_Url = portofolio.Img_Url,
                    Url = portofolio.Url,
                    Industry_Id = portofolio.Industry_Id,
                    Description = portofolio.Description,
                    type = portofolio.type
                });

                return response;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while retrieving all portofolios.");
                throw new InvalidOperationException("An error occurred while retrieving portofolios.", ex);
            }
        }


        public async Task<PortofolioUpdateReq> GetPortofolioById(int id)
        {
            try
            {
                var portofolio = await _unitOfWork.Repository<Portofolio>().GetAsync(id);
                if (portofolio is null) return null;
                var response = new PortofolioUpdateReq
                {
                    Id=portofolio.Id,
                    CLient_Id = portofolio.CLient_Id,
                    Img_Url = portofolio.Img_Url,
                    Url=portofolio.Url,
                    Industry_Id = portofolio.Industry_Id,
                    Description = portofolio.Description,
                    type = portofolio.type
                };

                return response ?? null;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while retrieving a portofolio by ID.");
                throw new InvalidOperationException($"An error occurred while retrieving the portofolio with ID {id}.", ex);
            }
        }

     

        public async Task<PortofolioUpdateReq> AddPortofio(PorotofolioAddReq portofolio)
        {
            try
            {
                if (portofolio == null)
                    throw new ArgumentNullException(nameof(portofolio), "Porotofolio data is null.");
                var checkClient = await _unitOfWork.Repository<Client>().GetAsync(portofolio.CLient_Id);
                if (checkClient == null)
                    throw new ArgumentNullException(nameof(portofolio), "Client  is not Exist.");
                var checkindustry = await _unitOfWork.Repository<Industry>().GetAsync(portofolio.Industry_Id);
                if (checkindustry == null)
                    throw new ArgumentNullException(nameof(portofolio), "Industry is not Exist.");

                var response = new Portofolio
                {
                    CLient_Id = portofolio.CLient_Id,
                    Img_Url = portofolio.Img_Url,
                    Industry_Id = portofolio.Industry_Id,
                    Description = portofolio.Description,
                    type = portofolio.type
                };

                await _unitOfWork.Repository<Portofolio>().AddAsync(response);
                await _unitOfWork.CompleteAsync();

                return new PortofolioUpdateReq
                {
                    Id=response.Id,
                    CLient_Id = portofolio.CLient_Id,
                    Img_Url = portofolio.Img_Url,
                    Url = portofolio.Url,
                    Industry_Id = portofolio.Industry_Id,
                    Description = portofolio.Description,
                    type = portofolio.type
                };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while adding a Portofolio.");
                throw new InvalidOperationException("An error occurred while adding the Portofolio.", ex);
            }
        }
        
        public async Task<PortofolioUpdateReq> UpdatePortofolio(PortofolioUpdateReq portofolio)
        {
            try
            {
                if (portofolio == null)
                    throw new ArgumentNullException(nameof(portofolio), "Client update data is null.");

                var existingportofolio = await _unitOfWork.Repository<Portofolio>().GetAsync(portofolio.Id);
                if (existingportofolio == null)
                    return null;
                var checkClient = await _unitOfWork.Repository<Client>().GetAsync(portofolio.CLient_Id);
                if (checkClient == null)
                    throw new ArgumentNullException(nameof(portofolio), "Client  is not Exist.");
                var checkindustry = await _unitOfWork.Repository<Industry>().GetAsync(portofolio.Industry_Id);
                if (checkindustry == null)
                    throw new ArgumentNullException(nameof(portofolio), "Industry is not Exist.");

                existingportofolio.Img_Url = portofolio.Img_Url;
                existingportofolio.CLient_Id = portofolio.CLient_Id;
                existingportofolio.Industry_Id = portofolio.Industry_Id;
                existingportofolio.Description = portofolio.Description;
                existingportofolio.Url = portofolio.Url;
                existingportofolio.type = portofolio.type;
               
               
                await _unitOfWork.Repository<Portofolio>().Update(existingportofolio);
                await _unitOfWork.CompleteAsync();

                return portofolio;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while updating a portofolio.");
                throw new InvalidOperationException($"An error occurred while updating the portotflio with ID {portofolio.Id}.", ex);
            }
        }






        public async Task<bool> DeleteAllPortofolio()
        {
            try
            {
                var portofolios = await _unitOfWork.Repository<Portofolio>().GetAllAsync();
                if (portofolios == null || !portofolios.Any())
                    return false;

                _unitOfWork.Repository<Portofolio>().DeleteAll();
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while deleting all portoflios.");
                return false;
            }
        }

        public async Task<bool> DeletePortofolioById(int id)
        {
            try
            {
                var portofolioRepository = _unitOfWork.Repository<Portofolio>();
                var portofolio = await portofolioRepository.GetAsync(id);
                if (portofolio == null)
                    return false;

                portofolioRepository.Delete(portofolio);
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



       
    }
}
