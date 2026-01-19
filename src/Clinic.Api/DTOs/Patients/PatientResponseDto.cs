using Clinic.Domain.Entities.Enums;
using Clinic.Domain.Entities;

namespace Clinic.Api.DTOs.Patients
{
    public class PatientResponseDto
    {
        public int PatientId { get; set; }
        public string FullName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string? Address { get; set; } = null!;
        public string NationalId { get; set; } = null!;
    }
}
