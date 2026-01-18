using Clinic.Domain.Entities;
using Clinic.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            //Primary Key
            builder.HasKey(x => x.UserId);
            //Property
            builder.Property(x => x.UserName)
                .IsRequired().HasMaxLength(100);
            builder.Property(x => x.UserEmail)
                .IsRequired().HasMaxLength(150);
            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            //Status (Enum)
            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(UserStatus.PendingVerification);


            //Index
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.HasIndex(x => x.UserEmail).IsUnique();


            //Relationship
            builder
                .HasOne(x=>x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);





        }
    }
}
