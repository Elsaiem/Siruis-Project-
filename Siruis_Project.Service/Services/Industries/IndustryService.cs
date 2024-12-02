using Siruis_Project.Core;
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

        public async Task<IEnumerable<Industry>> GetAllIndustries()
        {
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of Work is not initialized.");
            }

            var IndustryRepository = _unitOfWork.Repository<Industry>();
            if (IndustryRepository == null)
            {
                throw new InvalidOperationException("Client repository is not initialized.");
            }

            var result = await IndustryRepository.GetAllAsync();
            return result ?? Enumerable.Empty<Industry>();
        }

        public async Task<Industry> GetIndustryById(int id)
        {
            var Industry = await _unitOfWork.Repository<Industry>().GetAsync(id);
            if (Industry == null) return null;
            return Industry;
        }


        public async Task<Industry> AddIndustry(Industry industry)
        {
            if (industry == null) return null;


            await _unitOfWork.Repository<Industry>().AddAsync(industry);
            await _unitOfWork.CompleteAsync();
            return industry;
        }

        public async Task DeleteAllIndustries()
        {
            _unitOfWork.Repository<Industry>().DeleteAll();
            await _unitOfWork.CompleteAsync(); // Save the changes to the database
        }

        public async Task DeleteIndustryById(int id)
        {
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of Work is not initialized.");
            }

            var IndustryRepository = _unitOfWork.Repository<Industry>();
            if (IndustryRepository == null)
            {
                throw new InvalidOperationException("Industry repository is not initialized.");
            }

            var result = await IndustryRepository.GetAsync(id);

            if (result == null)
            {
                throw new InvalidOperationException($"Industry with id {id} does not exist.");
            }

            IndustryRepository.Delete(result);
            await _unitOfWork.CompleteAsync(); // Save the changes
        }

      
        public async Task<Industry> UpdateIndustry(Industry industry)
        {
            var check = await _unitOfWork.Repository<Industry>().GetAsync(industry.Id);
            if (check is null) return null;

            // Update properties explicitly
            check.Indust_Name = industry.Indust_Name;
           

            await _unitOfWork.Repository<Industry>().Update(check);
            await _unitOfWork.CompleteAsync();
            return check; // Return the updated entity
        }
    }
}
