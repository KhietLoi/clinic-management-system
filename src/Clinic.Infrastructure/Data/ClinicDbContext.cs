using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Use Entities in Domain
using Clinic.Domain.Entities;

namespace Clinic.Infrastructure.Data
{
    public class ClinicDbContext: DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
        {

        }
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Specialty> Specialties => Set<Specialty>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<PrescriptionItem> PrescriptionItems => Set<PrescriptionItem>();
        public DbSet<Prescription> Prescriptions => Set<Prescription>();
        public DbSet<Medicine>Medicines => Set<Medicine>();
        public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();

        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<ClinicRoom> ClinicRooms => Set<ClinicRoom>();
        public DbSet<Appointment> Appointments => Set<Appointment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
