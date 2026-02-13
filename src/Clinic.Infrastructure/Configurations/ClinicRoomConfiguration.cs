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
    public class ClinicRoomConfiguration : IEntityTypeConfiguration<ClinicRoom>
    {
        public void Configure(EntityTypeBuilder<ClinicRoom> builder)
        {
            builder.ToTable("ClinicRooms");
            //Primary Key
            builder.HasKey(x => x.ClinicRoomId);
            //RoomCode
            builder.Property(x => x.RoomCode)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(x => x.RoomCode)
                .IsUnique();

            //Status enum -> tinyint:
            builder.Property(x => x.Status)
                .HasConversion<byte>()
                .IsRequired();
             

            //Name
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.HasIndex(x=> x.Name).IsUnique();



            //Optional fields
            builder.Property(x => x.Location).HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(500);

           



            /*  //1-n Appointments
              builder
                  .HasMany(x=>x.Appointments)
                  .WithOne(x => x.ClinicRoom)
                  .HasForeignKey(x => x.ClinicRoomId)
                  .OnDelete(DeleteBehavior.Restrict);*/



        }
    }
}
