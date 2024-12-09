using Siruis_Project.Core.Dtos.IndustryDto;
using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.ServiceContract
{
    public interface IIndustryServices
    {
        Task<IEnumerable<IndustryUpdateReq>> GetAllIndustries();


        Task<IndustryUpdateReq> GetIndustryById(int id);


        Task<IndustryUpdateReq> AddIndustry(IndustryAddReq  industry);


        Task<bool> DeleteIndustryById(int id);

        Task<IndustryUpdateReq> UpdateIndustry(IndustryUpdateReq  industry);

        Task<bool> DeleteAllIndustries();





    }

}
