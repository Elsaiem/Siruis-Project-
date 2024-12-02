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
        Task<IEnumerable<Industry>> GetAllIndustries();


        Task<Industry> GetIndustryById(int id);


        Task<Industry> AddIndustry(Industry  industry);


        Task DeleteIndustryById(int id);

        Task<Industry> UpdateIndustry(Industry  industry);

        Task DeleteAllIndustries();





    }

}
