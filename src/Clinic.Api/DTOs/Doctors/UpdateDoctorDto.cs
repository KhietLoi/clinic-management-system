using System.ComponentModel.DataAnnotations;
using Clinic.Domain.Entities.Enums;
namespace Clinic.Api.DTOs.Doctors
{
    public class UpdateDoctorDto
    {
        [Required, MaxLength(200)]
        public string FullName { get; set; } = null!;

        [Required, MaxLength(20)]
        [Phone]
        public string Phone { get; set; } = null!;

        [MaxLength(150)]
        [EmailAddress]
        public string? Email { get; set; }

        [Range(0,60)]
        public int YearOfExperience { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public Gender gender { get; set; }
        
        public int? SpecialtyId { get; set; }


    }
}
