using Clinic.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Api.DTOs.Patients
{
    public class UpdatePatientDto
    {

        [Required, MaxLength(200)]
        public string FullName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; } = Gender.Unknown;

        [Required, MaxLength(20)]
        public string Phone { get; set; } = null!;

        [EmailAddress, MaxLength(150)]
        public string? Email { get; set; }
        [MaxLength(200)]
        public string? Address { get; set; } = null!;

        [Required, MaxLength(12)]
        public string NationalId { get; set; } = null!;
    }
}
