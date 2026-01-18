using Clinic.Domain.Entities;
using Clinic.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Data
{
    public static class ClinicDbSeeder
    {

        public static async Task SeedAsync(ClinicDbContext db)
        {
            // Nếu DB đã migrate rồi thì bạn có thể comment dòng này
            await db.Database.MigrateAsync();

            // Seed 1 lần thôi (đã có Users là coi như seed rồi)
            if (await db.Users.AnyAsync()) return;

            var now = DateTime.Now;

            // =========================
            // 1) Roles
            // =========================
            var roleAdmin = new Role { Name = "Admin", Description = "System administrator" };
            var roleDoctor = new Role { Name = "Doctor", Description = "Doctor account" };
            var roleStaff = new Role { Name = "Staff", Description = "Receptionist / staff" };

            db.Roles.AddRange(roleAdmin, roleDoctor, roleStaff);
            await db.SaveChangesAsync();

            // =========================
            // 2) Specialties
            // =========================
            var spDerm = new Specialty { Name = "Dermatology", Description = "Skin", CreatedAt = now, UpdatedAt = now };
            var spCard = new Specialty { Name = "Cardiology", Description = "Heart", CreatedAt = now, UpdatedAt = now };
            var spGen = new Specialty { Name = "General", Description = "General practice", CreatedAt = now, UpdatedAt = now };

            db.Specialties.AddRange(spDerm, spCard, spGen);
            await db.SaveChangesAsync();

            // =========================
            // 3) ClinicRooms
            // =========================
            var room101 = new ClinicRoom { Name = "Room 101", Location = "Floor 1", Description = "General exam room", CreatedAt = now, UpdatedAt = now };
            var room201 = new ClinicRoom { Name = "Room 201", Location = "Floor 2", Description = "Specialist room", CreatedAt = now, UpdatedAt = now };

            db.ClinicRooms.AddRange(room101, room201);
            await db.SaveChangesAsync();

            // =========================
            // 4) Users
            // =========================
            // NOTE: PasswordHash plain cho mock (sau đổi hash)
            var uAdmin = new User
            {
                UserName = "admin",
                UserEmail = "admin@clinic.local",
                PasswordHash = "admin123",
                Status = UserStatus.Active,
                CreatedAt = now,
                UpdatedAt = now,
                RoleId = roleAdmin.RoleId
            };

            var uStaff = new User
            {
                UserName = "staff01",
                UserEmail = "staff01@clinic.local",
                PasswordHash = "staff123",
                Status = UserStatus.Active,
                CreatedAt = now,
                UpdatedAt = now,
                RoleId = roleStaff.RoleId
            };

            var uDoctor1 = new User
            {
                UserName = "doctor01",
                UserEmail = "doctor01@clinic.local",
                PasswordHash = "doctor123",
                Status = UserStatus.Active,
                CreatedAt = now,
                UpdatedAt = now,
                RoleId = roleDoctor.RoleId
            };

            var uDoctor2 = new User
            {
                UserName = "doctor02",
                UserEmail = "doctor02@clinic.local",
                PasswordHash = "doctor123",
                Status = UserStatus.Active,
                CreatedAt = now,
                UpdatedAt = now,
                RoleId = roleDoctor.RoleId
            };

            db.Users.AddRange(uAdmin, uStaff, uDoctor1, uDoctor2);
            await db.SaveChangesAsync();

            // =========================
            // 5) Doctors
            // =========================
            var d1 = new Doctor
            {
                FullName = "Dr. Nguyen Van A",
                Phone = "0900000001",
                Email = "doctor01@clinic.local",
                YearOfExperience = 6,
                DateOfBirth = new DateOnly(1990, 2, 10),
                Gender = Gender.Male,
                CreatedAt = now,
                UpdatedAt = now,
                SpecialtyId = spDerm.SpecialtyId,
                UserId = uDoctor1.UserId
            };

            var d2 = new Doctor
            {
                FullName = "Dr. Tran Thi B",
                Phone = "0900000002",
                Email = "doctor02@clinic.local",
                YearOfExperience = 3,
                DateOfBirth = new DateOnly(1995, 7, 20),
                Gender = Gender.Female,
                CreatedAt = now,
                UpdatedAt = now,
                SpecialtyId = spGen.SpecialtyId,
                UserId = uDoctor2.UserId
            };

            db.Doctors.AddRange(d1, d2);
            await db.SaveChangesAsync();

            // =========================
            // 6) Patients
            // =========================
            var p1 = new Patient
            {
                FullName = "Tran Thi C",
                DateOfBirth = new DateOnly(2002, 5, 10),
                Gender = Gender.Female,
                Phone = "0900000101",
                Email = "patient01@clinic.local",
                Address = "Ho Chi Minh City",
                NationalId = "012345678901",
                CreatedAt = now,
                UpdatedAt = now
            };

            var p2 = new Patient
            {
                FullName = "Le Van D",
                DateOfBirth = new DateOnly(1998, 11, 20),
                Gender = Gender.Male,
                Phone = "0900000102",
                Email = "patient02@clinic.local",
                Address = "Ho Chi Minh City",
                NationalId = "012345678902",
                CreatedAt = now,
                UpdatedAt = now
            };

            var p3 = new Patient
            {
                FullName = "Pham E",
                DateOfBirth = new DateOnly(1988, 1, 15),
                Gender = Gender.Other,
                Phone = "0900000103",
                Email = "patient03@clinic.local",
                Address = "Binh Duong",
                NationalId = "012345678903",
                CreatedAt = now,
                UpdatedAt = now
            };

            db.Patients.AddRange(p1, p2, p3);
            await db.SaveChangesAsync();

            // =========================
            // 7) Medicines
            // =========================
            var m1 = new Medicine { Name = "Paracetamol 500mg", Unit = "Tablet", Description = "Fever reducer", CreatedAt = now, UpdatedAt = now };
            var m2 = new Medicine { Name = "Amoxicillin 500mg", Unit = "Capsule", Description = "Antibiotic", CreatedAt = now, UpdatedAt = now };
            var m3 = new Medicine { Name = "Cetirizine 10mg", Unit = "Tablet", Description = "Antihistamine", CreatedAt = now, UpdatedAt = now };
            var m4 = new Medicine { Name = "Omeprazole 20mg", Unit = "Capsule", Description = "Stomach protection", CreatedAt = now, UpdatedAt = now };

            db.Medicines.AddRange(m1, m2, m3, m4);
            await db.SaveChangesAsync();

            // =========================
            // 8) Appointments
            // =========================
            var ap1Time = now.AddHours(2);
            var ap2Time = now.AddHours(3);
            var ap3Time = now.AddDays(1).AddHours(1);

            var a1 = new Appointment
            {
                ScheduledAt = ap1Time,
                CheckInAt = null,
                Status = AppointmentStatus.Completed,
                Reason = "Skin rash consultation",
                IsWalkIn = false,
                CreatedAt = now,
                UpdatedAt = now,
                PatientId = p1.PatientId,
                DoctorId = d1.DoctorId,
                ClinicRoomId = room201.ClinicRoomId
            };

            var a2 = new Appointment
            {
                ScheduledAt = ap2Time,
                CheckInAt = now.AddHours(3).AddMinutes(-5),
                Status = AppointmentStatus.InProgress,
                Reason = "Walk-in general checkup",
                IsWalkIn = true,
                CreatedAt = now,
                UpdatedAt = now,
                PatientId = p2.PatientId,
                DoctorId = d2.DoctorId,
                ClinicRoomId = room101.ClinicRoomId
            };

            var a3 = new Appointment
            {
                ScheduledAt = ap3Time,
                CheckInAt = null,
                Status = AppointmentStatus.Cancelled,
                Reason = "Cancelled by patient",
                IsWalkIn = false,
                CreatedAt = now,
                UpdatedAt = now,
                PatientId = p3.PatientId,
                DoctorId = d1.DoctorId,
                ClinicRoomId = room201.ClinicRoomId
            };

            db.Appointments.AddRange(a1, a2, a3);
            await db.SaveChangesAsync();

            // =========================
            // 9) MedicalRecords
            // =========================
            var mr1 = new MedicalRecord
            {
                VisitDate = ap1Time,
                StartAt = ap1Time.AddMinutes(10),
                EndAt = ap1Time.AddMinutes(25),
                Symptomps = "Red rash, itching",
                Diagnosis = "Allergic dermatitis",
                Notes = "Avoid allergens, keep skin moisturized",
                CreatedAt = now,
                UpdatedAt = now,
                PatientId = p1.PatientId,
                DoctorId = d1.DoctorId,
                AppointmentId = a1.AppointmentId
            };

            var mr2 = new MedicalRecord
            {
                VisitDate = ap2Time,
                StartAt = ap2Time.AddMinutes(5),
                EndAt = null,
                Symptomps = "Headache, mild fever",
                Diagnosis = "Viral infection (suspected)",
                Notes = "Follow up if fever persists",
                CreatedAt = now,
                UpdatedAt = now,
                PatientId = p2.PatientId,
                DoctorId = d2.DoctorId,
                AppointmentId = a2.AppointmentId
            };

            db.MedicalRecords.AddRange(mr1, mr2);
            await db.SaveChangesAsync();

            // =========================
            // 10) Prescriptions (1-1 with MedicalRecord)
            // =========================
            var pr1 = new Prescription
            {
                MedicalRecordId = mr1.MedicalRecordId,
                CreatedAt = now,
                Notes = "Take after meals"
            };

            var pr2 = new Prescription
            {
                MedicalRecordId = mr2.MedicalRecordId,
                CreatedAt = now,
                Notes = "Drink plenty of water"
            };

            db.Prescriptions.AddRange(pr1, pr2);
            await db.SaveChangesAsync();

            // =========================
            // 11) PrescriptionItems
            // =========================
            var items = new List<PrescriptionItem>
            {
                new PrescriptionItem
                {
                    PrescriptionId = pr1.PrescriptionId,
                    MedicineId = m3.MedicineId,
                    Dosage = "1 tablet at night",
                    Quantity = 7,
                    Instructions = "Take before sleep",
                    Notes = "May cause drowsiness"
                },
                new PrescriptionItem
                {
                    PrescriptionId = pr1.PrescriptionId,
                    MedicineId = m1.MedicineId,
                    Dosage = "1 tablet x 2 times/day",
                    Quantity = 10,
                    Instructions = "Morning and evening"
                },
                new PrescriptionItem
                {
                    PrescriptionId = pr2.PrescriptionId,
                    MedicineId = m1.MedicineId,
                    Dosage = "1 tablet when fever > 38C",
                    Quantity = 6,
                    Instructions = "Max 3 tablets/day"
                },
                new PrescriptionItem
                {
                    PrescriptionId = pr2.PrescriptionId,
                    MedicineId = m4.MedicineId,
                    Dosage = "1 capsule before breakfast",
                    Quantity = 14,
                    Instructions = "Use for stomach protection"
                }
            };

            db.PrescriptionItems.AddRange(items);

            await db.SaveChangesAsync();
        }
    }
}
