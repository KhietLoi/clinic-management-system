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
    public class ClinicRoomConfiguration : IEntityTypeConfiguration<ClinicRoom>
    {
        public void Configure(EntityTypeBuilder<ClinicRoom> builder)
        {
            builder.ToTable("ClinicRooms");
            //Primary Key
            builder.HasKey(x => x.ClinicRoomId);
            //Property:

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.HasIndex(x=> x.Name).IsUnique();

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
