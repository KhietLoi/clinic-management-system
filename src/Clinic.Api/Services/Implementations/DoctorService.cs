using Clinic.Api.DTOs.Doctors;
using Clinic.Api.Services.Interfaces;
using Clinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Api.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly ClinicDbContext _clinicdb;

        public DoctorService (ClinicDbContext clinicdb)
        {
            _clinicdb = clinicdb;
        }
        public Task CreateAsync(CreateDoctordto dto)
        {
            //Dua vao 
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        //Tra ve tat ca danh sach cac bac si
        public async Task<List<DoctorResponseDto>> GetAllAsync()
        {
            return await _clinicdb.Doctors
                .AsNoTracking()
                .Select (d => new DoctorResponseDto
                {
                    DoctorId = d.DoctorId,
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

        public Task UpdateAsync(int id, UpdateDoctorDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
