using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.ClientDto;
using Siruis_Project.Core.Dtos.IndustryDto;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Service.Services.Industries
{
    public class IndustryService : IIndustryServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndustryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<IndustryUpdateReq>> GetAllIndustries()
        {
            try
            {
                var industryRepository = _unitOfWork.Repository<Industry>();
                var industries = await industryRepository.GetAllAsync();
                if (industries == null || !industries.Any())
                    return Enumerable.Empty<IndustryUpdateReq>();

                var response = industries.Select(industry => new IndustryUpdateReq
                {
                    Id = industry.Id,
                    Indust_Name = industry.Indust_Name
                });

                return response;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while retrieving all industries.");
                throw new InvalidOperationException("An error occurred while retrieving industries.", ex);
            }
        }


        public async Task<IndustryUpdateReq> GetIndustryById(int id)
        {
            try
            {
                var industry = await _unitOfWork.Repository<Industry>().GetAsync(id);
                if (industry is null) return null;
                var response = new IndustryUpdateReq
                {
                    Id = industry.Id,
                    Indust_Name = industry.Indust_Name,
                  

                };

                return response ?? null;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while retrieving a Industry by ID.");
                throw new InvalidOperationException($"An error occurred while retrieving the Industry with Id {id}.", ex);
            }
        }


        public async Task<IndustryUpdateReq> AddIndustry(IndustryAddReq industry)
        {
            try
            {
                if (industry == null)
                    throw new ArgumentNullException(nameof(industry), "Client data is null.");

                var newIndustry = new Industry
                {
                    Indust_Name = industry.Indust_Name,
                  
                };

                await _unitOfWork.Repository<Industry>().AddAsync(newIndustry);
                await _unitOfWork.CompleteAsync();

                return new IndustryUpdateReq
                {
                    Id = newIndustry.Id,
                    Indust_Name = newIndustry.Indust_Name,
                   
                };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while adding a Industry.");
                throw new InvalidOperationException("An error occurred while adding the Industry.", ex);
            }
        }

        public async Task<bool> DeleteAllIndustries()
        {
            try
            {
                var industries = await _unitOfWork.Repository<Industry>().GetAllAsync();
                if (industries == null || !industries.Any())
                    return false;

                _unitOfWork.Repository<Industry>().DeleteAll();
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while deleting all Industries.");
                return false;
            }
        }

        public async Task<bool> DeleteIndustryById(int id)
        {
            try
            {
                var IndustryRepository = _unitOfWork.Repository<Industry>();
                var industry = await IndustryRepository.GetAsync(id);
                if (industry == null)
                    return false;

                IndustryRepository.Delete(industry);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while deleting a industry by ID.");
                return false;
            }
        }

      
        public async Task<IndustryUpdateReq> UpdateIndustry(IndustryUpdateReq industry)
        {
            try
            {
                if (industry == null)
                    throw new ArgumentNullException(nameof(industry), "Industry update data is null.");

                var existingIndustry = await _unitOfWork.Repository<Industry>().GetAsync(industry.Id);
                if (existingIndustry == null)
                    return null;

                existingIndustry.Indust_Name = industry.Indust_Name;
              
                await _unitOfWork.Repository<Industry>().Update(existingIndustry);
                await _unitOfWork.CompleteAsync();

                return industry;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while updating a industry.");
                throw new InvalidOperationException($"An error occurred while updating the client with ID {industry.Id}.", ex);
            }
        }
    }
}
