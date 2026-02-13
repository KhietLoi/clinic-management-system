using Clinic.Api.DTOs.ClinicRooms;
using Clinic.Api.Services.Interfaces;
using Clinic.Domain.Entities;
using Clinic.Domain.Entities.Enums;
using Clinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Clinic.Api.Services.Implementations
{
    public class ClinicRoomService : IClinicRoomService
    {
        private readonly ClinicDbContext _clinicDb;

        public  ClinicRoomService (ClinicDbContext clinicDb)
        {
            _clinicDb = clinicDb;
        }
        public async Task<List<ClinicRoomResponseDto>> GetAllAsync()
        {
            return await _clinicDb.ClinicRooms
                .AsNoTracking()
                .Select(r => new ClinicRoomResponseDto
                {
                    ClinicRoomId = r.ClinicRoomId,
                    RoomCode = r.RoomCode,
                    Name = r.Name,
                    SpecialtyId = r.SpecialtyId,
                    SpecialtyName = r.Specialty.Name,
                    Status = r.Status,
                    Location = r.Location,
                    Description = r.Description
                }).ToListAsync();
           
        }

        public async Task<ClinicRoomResponseDto?> GetByIdAsync(int id)
        {
            return await _clinicDb.ClinicRooms
                .AsNoTracking()
                .Where(d => d.ClinicRoomId == id)
                .Select(d => new ClinicRoomResponseDto
                {
                    ClinicRoomId = d.ClinicRoomId,
                    RoomCode = d.RoomCode,
                    Name = d.Name,
                    SpecialtyId = d.SpecialtyId,
                    SpecialtyName= d.Specialty.Name,
                    Status = d.Status,
                    Location = d.Location,
                    Description = d.Description
                }).FirstOrDefaultAsync();

        }
        public async Task ChangeClinicRoomStatusAsync(int id, ClinicRoomStatus status)
        {
            var room = await _clinicDb.ClinicRooms
                .FirstOrDefaultAsync(r => r.ClinicRoomId == id);

            if (room == null)
                throw new KeyNotFoundException("Clinic room not found.");

            // (Optional) tránh update vô nghĩa
            if (room.Status == status)
                return;

            room.Status = status;

            await _clinicDb.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(CreateClinicRoomDto dto)
        {
            //Kiểm tra Specialty tồn tại:
            var specialty = await _clinicDb.Specialties
                .FirstOrDefaultAsync(s => s.SpecialtyId == dto.SpecialtyId);
            if(specialty == null)
            {
                throw new KeyNotFoundException("Specialty not found.");
            }
            // Tạo full prefix đúng format
            var fullPrefix = $"ROOM_{specialty.CodePrefix}";


            // Lấy RoomCode cuối cùng theo chuyên khoa
            var lastCode = await _clinicDb.ClinicRooms
                .Where(r => r.SpecialtyId == dto.SpecialtyId &&
                            r.RoomCode.StartsWith(fullPrefix))
                .OrderByDescending(r => r.RoomCode)
                .Select(r => r.RoomCode)
                .FirstOrDefaultAsync();


            int nextNumber = 1;

            if (!string.IsNullOrEmpty(lastCode))
            {
                // Cắt đúng phần số phía sau prefix
                var numberPart = lastCode.Substring(fullPrefix.Length);

                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
                else
                {
                    throw new InvalidOperationException(
                        $"Invalid RoomCode format: {lastCode}");
                }
            }

            // Sinh RoomCode mới
            var roomCode = $"{fullPrefix}_{nextNumber:D3}";


            //Tao Entity:
            var room = new ClinicRoom
            {
                RoomCode = roomCode,
                Name = dto.Name.Trim(),
                SpecialtyId = dto.SpecialtyId,
                Location = dto.Location,
                Description = dto.Description,
                Status = ClinicRoomStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _clinicDb.ClinicRooms.Add(room);
            await _clinicDb.SaveChangesAsync();
            return room.ClinicRoomId;

        }

        //Thêm điều kiện xóa khi hoàn thiện api:
        public async Task DeleteAsync(int id)
        {
            var room = await _clinicDb.ClinicRooms.FirstOrDefaultAsync(r => r.ClinicRoomId == id);

            if (room == null)
                throw new KeyNotFoundException("Clinic room not found.");

            _clinicDb.ClinicRooms.Remove(room);
            await _clinicDb.SaveChangesAsync();
        }

        //KTRA LẠI
        public async Task UpdateAsync(int id, UpdateClinicRoomDto dto)
        {
            // 1️⃣ Check phòng tồn tại không (nhẹ – nhanh)
            var exists = await _clinicDb.ClinicRooms
                .AnyAsync(r => r.ClinicRoomId == id);

            if (!exists)
                throw new KeyNotFoundException("Clinic room not found.");

            // 2️⃣ Update trực tiếp ở SQL (không load entity)
            await _clinicDb.ClinicRooms
                .Where(r => r.ClinicRoomId == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(r => r.Name, dto.Name.Trim())
                    .SetProperty(r => r.Location, dto.Location)
                    .SetProperty(r => r.Description, dto.Description)
                );
        }

    }
}
