using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");
            //Primary Key
            builder.HasKey(x => x.PatientId);

            //Rang buoc cac thuoc tinh:
            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Gender)
                .IsRequired();

            builder.Property(x=> x.Phone)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(x => x.Phone)
                .IsUnique();

            builder.Property(x => x.Email)
                .HasMaxLength(150);

            builder.Property(x => x.Address)
                .HasMaxLength(200);
            builder.Property(x => x.NationalId)
                .IsRequired()
                .HasMaxLength(12);

            //Quan he:

/*            //1-n with MedicalRecords
            builder
                .HasMany(x => x.MedicalRecords)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            //1-n with Appointments
            builder
                .HasMany(x => x.Appointments)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);*/





        }
    }
}
