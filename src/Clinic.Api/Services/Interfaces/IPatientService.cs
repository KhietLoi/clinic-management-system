using Clinic.Api.DTOs.Patients;

namespace Clinic.Api.Services.Interfaces
{
    public interface IPatientService
    {
        Task<(IReadOnlyList<PatientResponseDto> Items, int Total)> GetListAsync(string? keyword, string? phone, int page, int pageSize);

        Task<PatientResponseDto> GetByIdAsync(int id);

        Task<int> CreateAsync(CreatePatientDto dto);

        Task UpdateAsync(int id, UpdatePatientDto dto);

        Task DeleteAsync(int id);
    }
}
