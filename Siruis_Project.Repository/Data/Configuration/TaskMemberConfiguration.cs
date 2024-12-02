using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Repository.Data.Configuration
{
    public class TaskMemberConfiguration : IEntityTypeConfiguration<TaskMember>
    {
        public void Configure(EntityTypeBuilder<TaskMember> builder)
        {
            builder.Property(p => p.TeamMember_Id).IsRequired();
            builder.Property(p => p.TaskDesc).IsRequired();
            builder.Property(p => p.DateEnd).IsRequired();
            builder.Property(p => p.TeamMember_Id).IsRequired();

            builder.HasOne(t=>t.teamMember).WithMany(m=>m.tasks).HasForeignKey(t=>t.TeamMember_Id);



        }




    }
}
