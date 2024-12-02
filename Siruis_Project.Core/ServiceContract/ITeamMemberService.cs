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
        Task<IEnumerable< TeamMember>> GetAllMembers();

        Task<TeamMember> GetMemberById(int id);

        Task<TeamMember> AddMember(TeamMember member);

        Task DeleteMember(int id);

        Task<TeamMember> UpdateMember(TeamMember member);

       Task DeleteAllTeamMembers();


    }
}
