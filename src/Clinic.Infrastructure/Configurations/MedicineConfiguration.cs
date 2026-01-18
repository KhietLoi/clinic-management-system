using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Configurations
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Medicine> builder)
        {
            builder.ToTable("Medicines");
            //Primary Key
            builder.HasKey(builder => builder.MedicineId);

            //Property:
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Description)
                .HasMaxLength(500);
            builder.Property(x => x.Unit)
                .IsRequired()
                .HasMaxLength(200);

          /*  //1-n
            builder
                .HasMany(x => x.PrescriptionItems)
                .WithOne(x => x.Medicine)
                .HasForeignKey(x => x.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);*/


        }
    }
}
