using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core.Dtos.AuthDto;
using Siruis_Project.Core.Entities.Identity;
using Siruis_Project.Core.ServiceContract.IdentityServices;

namespace Siruis_Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
        {

            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("login")]// /api/Account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _userService.LoginAsync(login);

            if (user is null) { return Unauthorized("no user Found"); }

            return Ok(user);

        }

        [HttpPost("UpdateRole")]
        
        public async Task<ActionResult<UserDto>> UpdateRole([FromBody] UpdateRoleDto updateRoleDto)
        {
            var user = await _userService.UpdateRoleAsync(updateRoleDto);

            if (user is null) { return Unauthorized("no user Found"); }


            return Ok(user);  

            
            


        }







    }
}
