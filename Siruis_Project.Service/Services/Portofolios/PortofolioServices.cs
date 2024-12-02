using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.PortofolioD;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using System;
using System.Collections.Generic;
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


        public async Task<IEnumerable<Portofolio>> GetAllPortofolios()
        {
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of Work is not initialized.");
            }

            var PortofolioRepository = _unitOfWork.Repository<Portofolio>();
            if (PortofolioRepository == null)
            {
                throw new InvalidOperationException("Portofolio repository is not initialized.");
            }

            var result = await PortofolioRepository.GetAllAsync();
            return result ?? Enumerable.Empty<Portofolio>();
        }

        public async Task<Portofolio> GetPortofolioById(int id)
        {
            var portofolio = await _unitOfWork.Repository<Portofolio>().GetAsync(id);
            if (portofolio == null) return null;
            return portofolio;
        }

     

        public async Task<Portofolio> AddPortofio(PorotofolioAddReq portofolio)
        {
            if (portofolio == null) return null;

            var checkclient=_unitOfWork.Repository<Client>().GetAsync(portofolio.CLient_Id);
            if (checkclient is null) return null;
            var checkIndustry = _unitOfWork.Repository<Industry>().GetAsync(portofolio.Industry_Id);
            if (checkIndustry is null) return null;

            
            

            var response = new Portofolio
            {
                CLient_Id=portofolio.CLient_Id,
                Img_Url=portofolio.Img_Url,
                Industry_Id=portofolio.Industry_Id,
                Description = portofolio.Description,
                type= portofolio.type
            };
            

             await _unitOfWork.Repository<Portofolio>().AddAsync(response);
             await _unitOfWork.CompleteAsync();
            return response;
        }
        public async Task<Portofolio> UpdatePortofolio(PortofolioUpdateReq portofolio)
        {
            var check = await _unitOfWork.Repository<Portofolio>().GetAsync(portofolio.Id);
            if (check is null) return null;
            
            var checkclient = _unitOfWork.Repository<Client>().GetAsync(portofolio.CLient_Id);
            if (checkclient is null) return null;
            var checkIndustry = _unitOfWork.Repository<Industry>().GetAsync(portofolio.Industry_Id);
            if (checkIndustry is null) return null;

            check.CLient_Id = portofolio.CLient_Id;
            check.Industry_Id = portofolio.Industry_Id;
            check.Img_Url = portofolio.Img_Url;
            check.Description = portofolio.Description;
            check.type = portofolio.type;

            await _unitOfWork.Repository<Portofolio>().Update(check);
            await _unitOfWork.CompleteAsync();
            return check; // Return the updated entity
        }






        public async Task<bool> DeleteAllPortofolio()
        {
            _unitOfWork.Repository<Portofolio>().DeleteAll();
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task DeletePortofolioById(int id)
        {
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of Work is not initialized.");
            }

            var PortofolioRepository = _unitOfWork.Repository<Portofolio>();
            if (PortofolioRepository == null)
            {
                throw new InvalidOperationException("Portofolio repository is not initialized.");
            }

            var result = await PortofolioRepository.GetAsync(id);

            if (result == null)
            {
                throw new InvalidOperationException($"Portofolio with id {id} does not exist.");
            }

            PortofolioRepository.Delete(result);
            await _unitOfWork.CompleteAsync(); // Save the changes
        }



       
    }
}
