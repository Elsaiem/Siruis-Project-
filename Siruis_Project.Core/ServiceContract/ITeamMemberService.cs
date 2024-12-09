using Siruis_Project.Core.Dtos.TeamMemberDto;
using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.ServiceContract
{
    public interface ITeamMemberService
    {
        Task<IEnumerable<TeamMemberUpdateReq>> GetAllMembers();

        Task<TeamMemberUpdateReq> GetMemberById(int id);

        Task<TeamMemberUpdateReq> AddMember(TeamMemberAddReq member);

        Task<bool> DeleteMember(int id);

        Task<TeamMemberUpdateReq> UpdateMember(TeamMemberUpdateReq member);

       Task<bool> DeleteAllTeamMembers();


    }
}
