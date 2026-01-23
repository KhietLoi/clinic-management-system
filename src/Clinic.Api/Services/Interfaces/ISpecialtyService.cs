using Clinic.Api.DTOs.Specialties;
using Clinic.Domain.Entities.Enums;

namespace Clinic.Api.Services.Interfaces
{
    public interface ISpecialtyService
    {
        Task<IReadOnlyList<SpecialtyResponseDto>> GetAllAsync(string? keyword);
        Task<SpecialtyResponseDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(UpsertSpecialtyDto dto);
        Task<bool> UpdateAsync(int id, UpsertSpecialtyDto dto);
        Task<bool> DeleteAsync(int id);
        // Nghiep vu them, cap nhat trang thai khoa
        Task<bool> ChangeStatusAsync(int id, SpecialtyStatus status);


    }
}
