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
    public class PrescriptionItemConfiguration : IEntityTypeConfiguration<PrescriptionItem>
    {
        public void Configure(EntityTypeBuilder<PrescriptionItem> builder)
        {
            builder.ToTable("PrescriptionItems");

            //Primary Key:
            builder.HasKey(x => x.PrescriptionItemId);
            //property:
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Dosage).HasMaxLength(200);
            builder.Property(x => x.Instructions).HasMaxLength(500);

            builder.HasIndex(x => new { x.PrescriptionId, x.MedicineId }).IsUnique();

            //Relationship:
            builder.HasOne(x => x.Prescription)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.PrescriptionId)
            .OnDelete(DeleteBehavior.Restrict);



            builder.HasOne(x => x.Medicine)
                .WithMany(x => x.PrescriptionItems)
                .HasForeignKey(x => x.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
