using Clinic.Api.DTOs.Patients;
using Clinic.Api.Services.Interfaces;
using Clinic.Domain.Entities;
using Clinic.Domain.Entities.Enums;
using Clinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Clinic.Api.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly ClinicDbContext _db;
        public PatientService(ClinicDbContext db) => _db = db;

        public async Task<(IReadOnlyList<PatientResponseDto> Items, int Total)> GetListAsync(string? keyword, string? phone, int page, int pageSize)
        {
            //Kiemtra:
            if (page <= 0) page = 1;
            if(pageSize<=0) pageSize = 10;
            if(pageSize>=100) pageSize = 100;
            //Querry
           /* 
            AsNoTracking: EF khong theo doi entity, nhanh hon, phu hop voi doc du lieu
            */
            var q = _db.Patients.AsNoTracking().AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                q = q.Where(p => p.FullName.Contains(keyword) 
                ||(p.Email !=null && p.Email.Contains(keyword))
                ||p.NationalId.Contains(keyword));
            }
            if(!string.IsNullOrWhiteSpace(phone))
            {
                phone = phone.Trim();
                q = q.Where(p => p.Phone.Contains(phone));
            }

            var total = await q.CountAsync();

            var items = await q.OrderByDescending(p => p.PatientId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PatientResponseDto
                {
                    PatientId = p.PatientId,
                    NationalId = p.NationalId,
                    FullName = p.FullName,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    Phone = p.Phone,
                    Email = p.Email,
                    Address = p.Address
                })
                .ToListAsync();
            return (items,total) ;
        }
        public async Task<int> CreateAsync(CreatePatientDto dto)
        {
            var nationalid = dto.NationalId.Trim();
            

            var exists = await _db.Patients.AnyAsync(p => p.NationalId == nationalid);
            //neu da ton tai:
            if (exists)
            {
                throw new InvalidOperationException("NationalId  already exists.");
            }

            //Create new Patient
            var entity = new Patient
            {
                FullName = dto.FullName,
                NationalId = nationalid,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Phone = dto.Phone,
                Email = dto.Email?.Trim(),
                Address = dto.Address?.Trim(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow

            };
            _db.Patients.Add(entity);
            await _db.SaveChangesAsync();
            return entity.PatientId;
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
            if (patient == null)
                throw new KeyNotFoundException("Patient not found");

            var hasMedicalRecords = await _db.MedicalRecords
                .AnyAsync(m => m.PatientId == id);

            if (hasMedicalRecords)
                throw new InvalidOperationException("Cannot delete patient with medical history.");

            var hasBlockingAppointments = await _db.Appointments.AnyAsync(a =>
                a.PatientId == id &&
                a.Status != AppointmentStatus.Cancelled &&
                a.Status != AppointmentStatus.NoShow
            );

            if (hasBlockingAppointments)
                throw new InvalidOperationException(
                    "Cannot delete patient because active or completed appointments exist.");

            await using var tx = await _db.Database.BeginTransactionAsync();

            var appointmentsToDelete = await _db.Appointments
                .Where(a => a.PatientId == id)
                .ToListAsync();

            if (appointmentsToDelete.Count > 0)
                _db.Appointments.RemoveRange(appointmentsToDelete);

            _db.Patients.Remove(patient);

            await _db.SaveChangesAsync();
            await tx.CommitAsync();
        }


        public async Task<PatientResponseDto> GetByIdAsync(int id)
        {
            var patient = await _db.Patients.AsNoTracking()
                .Where(p => p.PatientId == id)
                .Select(p => new PatientResponseDto
                {
                    PatientId = p.PatientId,
                    NationalId = p.NationalId,
                    FullName = p.FullName,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    Phone = p.Phone,
                    Email = p.Email,
                    Address = p.Address
                })
                .FirstOrDefaultAsync();

            if (patient == null)
                throw new KeyNotFoundException("Patient not found");

            return patient;
        }



        public async Task UpdateAsync(int id, UpdatePatientDto dto)
        {
            var entity = await _db.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
            if (entity == null)
                throw new KeyNotFoundException("Patient not found");

            var phone = dto.Phone.Trim();
            var phoneExists = await _db.Patients
                .AnyAsync(p => p.Phone == phone && p.PatientId != id);

            if (phoneExists)
                throw new InvalidOperationException("Phone already exists.");

            var nationalid = dto.NationalId.Trim();
            var nationalidExists = await _db.Patients
                .AnyAsync(p => p.NationalId == nationalid && p.PatientId != id);

            if (nationalidExists)
                throw new InvalidOperationException("NationalId already exists.");

            entity.FullName = dto.FullName;
            entity.NationalId = nationalid;
            entity.DateOfBirth = dto.DateOfBirth;
            entity.Gender = dto.Gender;
            entity.Phone = phone;
            entity.Email = dto.Email?.Trim();
            entity.Address = dto.Address?.Trim();
            entity.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
        }

    }
}
