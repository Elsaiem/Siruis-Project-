using Microsoft.EntityFrameworkCore;
using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Repository.Data.Contexts
{
    public class SiruisDbContext:DbContext
    {
       public SiruisDbContext(DbContextOptions<SiruisDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }





        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Client> Clients { get; set; }
        
        public DbSet<TeamMember> TeamMembers { get; set; }

        public DbSet<TaskMember> Tasks { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Portofolio> Portos { get; set; }


        public DbSet<Industry> Industries { get; set; }






    }
}
