using Clinic.Api.DTOs.Doctors;

namespace Clinic.Api.Services.Interfaces
{
    public interface IDoctorService
    {
        Task <List<DoctorResponseDto>> GetAllAsync();
        Task <DoctorResponseDto?> GetByIdAsync (int id);

        Task <int> CreateAsync(CreateDoctordto dto);
        Task UpdateAsync (int id, UpdateDoctorDto dto);

        Task DeleteAsync (int id);
    }
}
