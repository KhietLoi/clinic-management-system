using Clinic.Domain.Entities;
using Clinic.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Configurations
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable("Specialties");
            //Primary Key:
            builder.HasKey(x => x.SpecialtyId);
            //Property:
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Property(x => x.IsActive)
                .HasDefaultValue(SpecialtyStatus.Active);
            //1-1
            builder.HasOne(s => s.ClinicRoom)
               .WithOne(r => r.Specialty)
               .HasForeignKey<ClinicRoom>(r => r.SpecialtyId)
               .OnDelete(DeleteBehavior.Restrict);

            /*  //1-n Doctors:
              builder
                  .HasMany(x => x.Doctors)
                  .WithOne(x => x.Specialty)
                  .HasForeignKey(x => x.SpecialtyId)
                  .OnDelete(DeleteBehavior.Restrict);*/

        }
    }
}
