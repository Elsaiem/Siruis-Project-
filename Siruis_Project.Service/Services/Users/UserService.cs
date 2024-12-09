using Microsoft.AspNetCore.Identity;
using Siruis_Project.Core.Dtos.AuthDto;
using Siruis_Project.Core.Entities.Identity;
using Siruis_Project.Core.ServiceContract.IdentityServices;
using System;
using System.Threading.Tasks;

namespace Siruis_Project.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<AppUser> userManager,
                           SignInManager<AppUser> signInManager,
                           ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null) return null;

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded) return null;

                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await _tokenService.CreateTokenAsync(user, _userManager),
                    Role = user.Role
                };
            }
            catch (Exception ex)
            {
                // Log the error if logging is implemented
                // Logger.LogError(ex, "Error occurred during login.");
                throw new InvalidOperationException("An error occurred while processing the login.", ex);
            }
        }

        public async Task<UserDto> UpdateRoleAsync(UpdateRoleDto updateRoleDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(updateRoleDto.email);
                if (user is null) return null;

                if (updateRoleDto.role is null) return null;

                user.Role = updateRoleDto.role;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded) return null;

                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await _tokenService.CreateTokenAsync(user, _userManager),
                    Role = user.Role
                };
            }
            catch (Exception ex)
            {
                // Log the error if logging is implemented
                // Logger.LogError(ex, "Error occurred while updating user role.");
                throw new InvalidOperationException("An error occurred while updating the user role.", ex);
            }
        }

        public async Task<bool> CheckEmailExistAsync(string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email) is null;
            }
            catch (Exception ex)
            {
                // Log the error if logging is implemented
                // Logger.LogError(ex, "Error occurred while checking email existence.");
                throw new InvalidOperationException("An error occurred while checking the email existence.", ex);
            }
        }
    }
}
