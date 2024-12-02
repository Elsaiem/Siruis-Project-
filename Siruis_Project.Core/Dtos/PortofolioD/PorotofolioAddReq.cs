using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Dtos.PortofolioD
{
    public record PorotofolioAddReq
    (
        int CLient_Id,
        string   Img_Url,
        int Industry_Id,
        string Description,
        Types type
    );
}
