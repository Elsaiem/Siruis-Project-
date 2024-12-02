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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(n => n.Name).IsRequired();
            builder.Property(n => n.Address).IsRequired();
            builder.Property(n => n.Phone).IsRequired();

            builder.Property(O => O.status)
                  .HasConversion(O => O.ToString(), Ostatus => (Status)Enum.Parse(typeof(Status), Ostatus));

            builder.Property(O => O.plan)
                .HasConversion(O => O.ToString(), plan => (Plan)Enum.Parse(typeof(Plan), plan));






        }
    }
}
