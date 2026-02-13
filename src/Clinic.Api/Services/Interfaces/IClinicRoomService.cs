using Clinic.Api.DTOs.ClinicRooms;
using Clinic.Domain.Entities.Enums;

namespace Clinic.Api.Services.Interfaces
{
    public interface IClinicRoomService
    {
        Task<List<ClinicRoomResponseDto>> GetAllAsync(); //Lay danh sach cac phong
        Task <ClinicRoomResponseDto?> GetByIdAsync(int id); // Lay mot phong cu the

        Task <int> CreateAsync(CreateClinicRoomDto dto); // Tao mot phong

        Task UpdateAsync (int id, UpdateClinicRoomDto dto); // Cap nhat mot phong

        Task DeleteAsync (int id); // Xoa mot phong
        //Them cap nhat trang thai phong nua:
        Task ChangeClinicRoomStatusAsync (int id, ClinicRoomStatus status);
    }
}
