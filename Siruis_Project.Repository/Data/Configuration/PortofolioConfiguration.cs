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
    public class PortofolioConfiguration : IEntityTypeConfiguration<Portofolio>
    {
        public void Configure(EntityTypeBuilder<Portofolio> builder)
        {

            builder.Property(p => p.CLient_Id).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Industry_Id).IsRequired();
            builder.Property(p => p.Img_Url).IsRequired();
            builder.Property(p=>p.type).IsRequired();

            builder.HasOne(p => p.client)
            .WithMany(c => c.portofolios) // Adjust navigation property
            .HasForeignKey(p => p.CLient_Id).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.industry)
            .WithMany(c => c.Portofolios) // Adjust navigation property
            .HasForeignKey(p => p.Industry_Id)
            .OnDelete(DeleteBehavior.Restrict);


            builder.Property(p => p.type)
              .HasConversion(p => p.ToString(), type => (Types)Enum.Parse(typeof(Types), type));




        }
    }
}
