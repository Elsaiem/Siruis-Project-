using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Dtos.PortofolioD
{
    public record PortofolioUpdateReq
    { 
          public int Id {  get; init; }
          public int CLient_Id {  get; init; }
          public string Img_Url {  get; init; }
          public string? Url {  get; init; }

          public int Industry_Id {  get; init; }
          public string Description {  get; init; }
          public  Types type {  get; init; }

        }
   
}
