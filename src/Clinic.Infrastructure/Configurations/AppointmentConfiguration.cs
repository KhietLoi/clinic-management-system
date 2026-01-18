using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");
            //PrimaryKey
            builder.HasKey(x => x.AppointmentId);
            //Not same schedule
            builder.HasIndex(x => new { x.DoctorId, x.ScheduledAt });
            builder.HasIndex(x => new { x.PatientId, x.ScheduledAt });

            //Property
            builder.Property(x => x.ScheduledAt)
                .IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Reason).HasMaxLength(500);

            //Relationship
            //1-N
            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Doctor)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ClinicRoom)
                .WithMany (x => x.Appointments)
                .HasForeignKey(x => x.ClinicRoomId)
                .OnDelete(DeleteBehavior.Restrict);

/*            //1-1
            builder
                .HasOne(x => x.MedicalRecord)
                .WithOne(x => x.Appointment)
                .HasForeignKey<MedicalRecord>(x => x.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);*/

        }
    }
}
