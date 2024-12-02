using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Siruis_Project.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Repository.Identity.Context
{
    public class SiruisIdentityDbContext : IdentityDbContext<AppUser>
    {
        public SiruisIdentityDbContext(DbContextOptions options) : base(options)
        {





        }
    }
}
