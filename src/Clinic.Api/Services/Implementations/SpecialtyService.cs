using Clinic.Api.DTOs.Specialties;
using Clinic.Api.Services.Interfaces;
using Clinic.Domain.Entities;
using Clinic.Domain.Entities.Enums;
using Clinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Api.Services.Implementations
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ClinicDbContext _clinicDb;

        public SpecialtyService (ClinicDbContext clinicDb)
        {
            _clinicDb = clinicDb;
        }

        

        //ADMIN HE THONG
        public async Task<int> CreateAsync(UpsertSpecialtyDto dto)
        {
            var name = dto.Name.Trim();

            var exists = await _clinicDb.Specialties.AnyAsync(x => x.Name.ToLower() == name.ToLower());

            if (exists)
            {
                throw new InvalidOperationException("Specialty name already exists.");
            }

            //Tao khoa moi:
            var entity = new Specialty
            {
                Name = name,
                Description = dto.Description?.Trim(),
                IsActive = SpecialtyStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _clinicDb.Specialties.Add(entity);

            await _clinicDb.SaveChangesAsync(); 

            return entity.SpecialtyId;
            

        }
        //ADMIN HE THONG
        public async Task DeleteAsync(int id)
        {
            var entity = await _clinicDb.Specialties
                .Include(s => s.ClinicRoom)
                .FirstOrDefaultAsync(s => s.SpecialtyId == id);

            if (entity == null)
                throw new KeyNotFoundException("Specialty not found.");

            if (entity.IsActive != SpecialtyStatus.Closed)
                throw new InvalidOperationException("Only closed specialties can be deleted.");

            var hasAnyDoctor = await _clinicDb.Doctors
                .AnyAsync(d => d.SpecialtyId == id);

            if (hasAnyDoctor)
                throw new InvalidOperationException("Cannot delete specialty because doctors exist.");

            if (entity.ClinicRoom != null)
                throw new InvalidOperationException("Unassign clinic room first.");

            _clinicDb.Specialties.Remove(entity);
            await _clinicDb.SaveChangesAsync();
        }


        //Lay danh sach Khoa
        public async Task<IReadOnlyList<SpecialtyResponseDto>> GetAllAsync(string? keyword)
        {
            var q = _clinicDb.Specialties.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                q = q.Where(s => s.Name.Contains(keyword));
            }

            return await q.OrderBy(s => s.Name)
                .Select(s => new SpecialtyResponseDto
                {
                    SpecialtyId = s.SpecialtyId,
                    Name = s.Name,
                    Description = s.Description,
                    IsActive = s.IsActive
                })
                .ToListAsync();
        }

        //Lay doi tuong mot khoa cu the thong qua ID
        public async Task<SpecialtyResponseDto> GetByIdAsync(int id)
        {
            var specialty = await _clinicDb.Specialties.AsNoTracking()
                .Where(s => s.SpecialtyId == id)
                .Select(s => new SpecialtyResponseDto
                {
                    SpecialtyId = s.SpecialtyId,
                    Name = s.Name,
                    Description = s.Description,
                    IsActive = s.IsActive
                })
                .FirstOrDefaultAsync();

            if (specialty == null)
                throw new KeyNotFoundException("Specialty not found.");

            return specialty;
        }


        //ADMIN HE THONG
        //Cap nhat thuoc tinh mot khoa:
        public async Task UpdateAsync(int id, UpsertSpecialtyDto dto)
        {
            var entity = await _clinicDb.Specialties
                .FirstOrDefaultAsync(s => s.SpecialtyId == id);

            if (entity == null)
                throw new KeyNotFoundException("Specialty not found.");

            var name = dto.Name.Trim();

            var exists = await _clinicDb.Specialties
                .AnyAsync(s => s.Name.ToLower() == name.ToLower() && s.SpecialtyId != id);

            if (exists)
                throw new InvalidOperationException("Specialty name already exists.");

            entity.Name = name;
            entity.Description = dto.Description?.Trim();
            entity.UpdatedAt = DateTime.UtcNow;

            await _clinicDb.SaveChangesAsync();
        }


        //ADMIN HE THONG
        //Thay doi trang thai mot khoa:
        public async Task ChangeStatusAsync(int id, SpecialtyStatus status)
        {
            var entity = await _clinicDb.Specialties
                .FirstOrDefaultAsync(s => s.SpecialtyId == id);

            if (entity == null)
                throw new KeyNotFoundException("Specialty not found.");

            if (status == SpecialtyStatus.Closed)
            {
                var hasActiveDoctors = await _clinicDb.Doctors.AnyAsync(d =>
                    d.SpecialtyId == id &&
                    d.User.Status == UserStatus.Active);

                if (hasActiveDoctors)
                    throw new InvalidOperationException(
                        "Cannot close specialty because active doctors exist.");
            }

            entity.IsActive = status;
            entity.UpdatedAt = DateTime.UtcNow;

            await _clinicDb.SaveChangesAsync();
        }

    }
}
