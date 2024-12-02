using Siruis_Project.Core.Dtos.AuthDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.ServiceContract.IdentityServices
{
    public interface IUserService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<bool> CheckEmailExistAsync(string email);
        Task<UserDto> UpdateRoleAsync(UpdateRoleDto updateRoleDto);
    }
}
