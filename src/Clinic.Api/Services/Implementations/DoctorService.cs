using Clinic.Api.DTOs.Doctors;
using Clinic.Api.Services.Interfaces;
using Clinic.Domain.Entities;
using Clinic.Domain.Entities.Enums;
using Clinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Clinic.Api.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly ClinicDbContext _clinicdb;

        public DoctorService (ClinicDbContext clinicdb)
        {
            _clinicdb = clinicdb;
        }
        public async Task<int> CreateAsync(CreateDoctordto dto)
        {
            //1. Check Specialty ton tai ko:
            var specialty = await _clinicdb.Specialties
                .FirstOrDefaultAsync(s => s.SpecialtyId == dto.SpecialtyId);

            if (specialty == null)
            {
                throw new KeyNotFoundException("Specialty not found.");
            }

            //2. Check User ton tai (neu moi doctor gan voi user login)
            var userExists = await _clinicdb.Users
                .AnyAsync(u => u.UserId == dto.UserId);

            if (!userExists)
            {
                throw new KeyNotFoundException("User not found.");
            }

            //3. Lay DoctorCode cuoi cung theo chuyen khoa:
            var lastCode = await _clinicdb.Doctors
                .Where(d => d.SpecialtyId == dto.SpecialtyId)
                .OrderByDescending(d => d.DoctorCode)
                .Select(d => d.DoctorCode)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (!string.IsNullOrEmpty(lastCode))
            {
                var numberPart = lastCode.Replace(specialty.CodePrefix, "");
                nextNumber = int.Parse(numberPart) + 1;
            }

            var doctorCode = $"{specialty.CodePrefix}{nextNumber:D3}";

            //Tao entity:
            var doctor = new Doctor
            {
                DoctorCode = doctorCode,
                FullName = dto.FullName.Trim(),
                Phone = dto.Phone.Trim(),
                Email = dto.Email?.Trim(),

                YearOfExperience = dto.YearOfExperience,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,

                SpecialtyId = dto.SpecialtyId,
                UserId = dto.UserId,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            //Save data:
            _clinicdb.Doctors.Add(doctor);
            await _clinicdb.SaveChangesAsync();
            return doctor.DoctorId;
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _clinicdb.Doctors
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.DoctorId == id);

            if (doctor == null)
            {
                throw new KeyNotFoundException("Doctor not found");
            }

            if(doctor.User.Status != UserStatus.PendingVerification)
            {
                throw new InvalidOperationException("Only unverified doctors can be deleted.");
            }

            //Xoa
            _clinicdb.Doctors.Remove(doctor);
            _clinicdb.Users.Remove(doctor.User);

            await _clinicdb.SaveChangesAsync();
        }

        //Tra ve tat ca danh sach cac bac si
        public async Task<List<DoctorResponseDto>> GetAllAsync()
        {
            return await _clinicdb.Doctors
                .AsNoTracking()
                .Select (d => new DoctorResponseDto
                {
                    DoctorId = d.DoctorId,
                    DoctorCode = d.DoctorCode,
                    FullName = d.FullName,
                    Phone = d.Phone,
                    Email = d.Email,
                    YearOfExperience = d.YearOfExperience,
                    DateOfBirth = d.DateOfBirth,
                    Gender = d.Gender,
                    SpecialtyId = d.SpecialtyId,
                    SpecialtyName = d.Specialty != null ? d.Specialty.Name : null,
                    CreatedAt = d.CreatedAt
                }) .ToListAsync();
            
        }

        public async Task<DoctorResponseDto?> GetByIdAsync(int id)
        {
            return await _clinicdb.Doctors
                .AsNoTracking()
                .Where(d => d.DoctorId == id)
                .Select(d => new DoctorResponseDto
                {
                    DoctorId = d.DoctorId,
                    DoctorCode = d.DoctorCode,
                    FullName = d.FullName,
                    Phone = d.Phone,
                    Email = d.Email,
                    YearOfExperience = d.YearOfExperience,
                    DateOfBirth = d.DateOfBirth,
                    Gender = d.Gender,
                    SpecialtyId = d.SpecialtyId,
                    SpecialtyName = d.Specialty != null ? d.Specialty.Name : null,
                    CreatedAt = d.CreatedAt
                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, UpdateDoctorDto dto)
        {
            //Xac dinh bac si:
            var doctor = await _clinicdb.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == id);
            if (doctor == null)
            {
                throw new KeyNotFoundException("Doctor not found");
            }

            //Kiem tra trung phone neu doi:
            if (doctor.Phone != dto.Phone && await _clinicdb.Doctors.AnyAsync(d => d.Phone == dto.Phone))
            {
                throw new InvalidOperationException("Phone already exists");
            }
            //Kiem tra email neu co:
            if(dto.Email != null)
            {
                if (doctor.Email != dto.Email && await _clinicdb.Doctors.AnyAsync(d => d.Email == dto.Email))
                {
                    throw new InvalidOperationException("Email already exists");
                }
            }

            //Cap nhat:
            doctor.FullName = dto.FullName;
            doctor.Phone = dto.Phone.Trim();
            doctor.Email = dto.Email?.Trim();
            doctor.YearOfExperience = dto.YearOfExperience;
            doctor.DateOfBirth = dto.DateOfBirth;
            doctor.Gender = dto.gender;
            
            doctor.UpdatedAt = DateTime.UtcNow;

            await _clinicdb.SaveChangesAsync();
           
           
        }
    }
}
