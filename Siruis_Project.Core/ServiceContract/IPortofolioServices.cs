using Siruis_Project.Core.Dtos.PortofolioD;
using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.ServiceContract
{
    public interface IPortofolioServices
    {
        Task<IEnumerable<Portofolio>> GetAllPortofolios();


        Task<Portofolio> GetPortofolioById(int id);


        Task<Portofolio> AddPortofio(PorotofolioAddReq portofolio);

        Task DeletePortofolioById(int id);

        Task<Portofolio> UpdatePortofolio(PortofolioUpdateReq portofolio);

        Task<bool> DeleteAllPortofolio();






    }
}
