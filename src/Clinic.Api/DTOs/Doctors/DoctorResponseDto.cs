using Clinic.Domain.Entities.Enums;

namespace Clinic.Api.DTOs.Doctors
{
    public class DoctorResponseDto
    {
        public int DoctorId { get; set; }
        public string DoctorCode { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }

        public int YearOfExperience { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public int? SpecialtyId { get; set; }
        public string? SpecialtyName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
