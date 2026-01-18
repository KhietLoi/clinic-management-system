using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Configurations
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.ToTable("MedicalRecords");

            //Primary Key:
            builder.HasKey(x => x.MedicalRecordId);

            //1-1 appointment - medical record
            builder.HasIndex(x => x.AppointmentId).IsUnique();
            //Property:
            builder.Property(x => x.Symptomps)
                .HasMaxLength(3000);
            builder.Property(x => x.Diagnosis)
                .HasMaxLength(3000);
            builder.Property(x => x.Notes)
                .HasMaxLength(3000);

            //Relationship:

            //1-1 Appointments
            builder.HasOne(m => m.Appointment)
           .WithOne(a => a.MedicalRecord)
           .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
           .OnDelete(DeleteBehavior.Restrict);

            //n-1 Patient
            builder
                .HasOne(m => m.Patient)
                .WithMany(x => x.MedicalRecords)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            //n-1 Doctor:
            builder
                .HasOne(m => m.Doctor)
                .WithMany (x => x.MedicalRecords)
                .HasForeignKey (x => x.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);






        }
    }
}
