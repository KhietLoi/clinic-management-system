using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");
            //Primary Key:
            builder.HasKey(x => x.DoctorId);

            //Property:
            builder.Property(x => x.FullName)
                .HasMaxLength(200);

            //Rang buoc cac thuoc tinh:
            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Gender)
                .IsRequired();

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(x => x.Phone)
                .IsUnique();

            builder.Property(x => x.Email)
                .HasMaxLength(150);


            builder.Property(x => x.YearOfExperience)
                .IsRequired();

            //Relationship:

            //1-1 User:
            builder
                .HasOne(x => x.User)
                .WithOne(x => x.Doctor)
                .HasForeignKey<Doctor>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //N-1 Specialty
            builder.HasOne(x => x.Specialty)
                .WithMany(x => x.Doctors)
                .HasForeignKey(x => x.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);





        }
    }
}
