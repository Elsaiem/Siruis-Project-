using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Dtos.IndustryDto
{
    public record IndustryUpdateReq
    {
        public int Id { get; init; }
        public string Indust_Name { get; init; }

    }
}
