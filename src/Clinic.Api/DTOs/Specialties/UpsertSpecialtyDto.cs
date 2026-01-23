using System.ComponentModel.DataAnnotations;

namespace Clinic.Api.DTOs.Specialties
{
    public class UpsertSpecialtyDto
    {

        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }


    }
}
