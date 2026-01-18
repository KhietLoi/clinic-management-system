using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Configurations
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("Prescriptions");
            //Primary Key
            builder.HasKey(x => x.PrescriptionId);
            //Property
            builder.Property(x => x.Notes)
                .HasMaxLength(5000);
            //Ralationship:
            //1-1:
            builder
                .HasOne(x => x.MedicalRecord)
                .WithOne(x => x.Prescription)
                .HasForeignKey<Prescription>(x => x.MedicalRecordId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
