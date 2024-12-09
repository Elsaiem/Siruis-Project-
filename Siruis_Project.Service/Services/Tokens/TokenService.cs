using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Siruis_Project.Core.Entities.Identity;
using Siruis_Project.Core.ServiceContract.IdentityServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Service.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> manager)
        {
            try
            {
                // 1- Header (algo, Type)
                // 2- Payload (claims)
                // 3- Signature 

                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.DisplayName),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                };

                // Get user roles and add to claims
                var userRole = await manager.GetRolesAsync(user);
                foreach (var role in userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                // Generate the JWT key
                var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                // Create the token
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );

                // Return the JWT as a string
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (ArgumentNullException argEx)
            {
                // Handle argument null exception
                throw new InvalidOperationException("A required argument is missing or invalid.", argEx);
            }
            catch (FormatException formatEx)
            {
                // Handle format exceptions (e.g., invalid numeric format for duration)
                throw new InvalidOperationException("Invalid format in configuration values.", formatEx);
            }
            catch (Exception ex)
            {
                // Log the error (if logging is implemented) and throw a general exception
                // Logger.LogError(ex, "An error occurred while generating the JWT token.");
                throw new InvalidOperationException("An error occurred while creating the token.", ex);
            }
        }
    }
}
