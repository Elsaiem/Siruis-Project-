using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.Dtos.TeamMemberDto
{
    public record TeamMemberUpdateReq
    {
        public int Id { get; init; }
        public string TeamName { get; init; }
        public string JobTitle { get; init; }


    }
}
