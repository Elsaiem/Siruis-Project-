using Microsoft.AspNetCore.Identity;
using Siruis_Project.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Repository.Identity
{
    public class SiruisProjectDbContextSeed
    {
        public static async  Task SeedSiruisUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    Email = "y.saiem2111@gmail.com",
                    DisplayName = "Yousef Elsaiem",
                    UserName = "Elsaiem",
                    PhoneNumber = "01023164133"
                 
                    
                     


                };
                await _userManager.CreateAsync(user,"P@ssW0rd");






            }





        }





    }
}
