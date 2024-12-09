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
        Task<IEnumerable<PortofolioUpdateReq>> GetAllPortofolios();


        Task<PortofolioUpdateReq> GetPortofolioById(int id);


        Task<PortofolioUpdateReq> AddPortofio(PorotofolioAddReq portofolio);

        Task<bool> DeletePortofolioById(int id);

        Task<PortofolioUpdateReq> UpdatePortofolio(PortofolioUpdateReq portofolio);

        Task<bool> DeleteAllPortofolio();






    }
}
