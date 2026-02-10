using System.ComponentModel.DataAnnotations;

namespace Clinic.Api.DTOs.Specialties
{
    public class UpsertSpecialtyDto
    {

        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        //Thêm codeprefix
        [Required, MaxLength(5)]
        [RegularExpression("^[A-Z]+$", ErrorMessage = "CodePrefix must contain only uppercase letters")]
        public string CodePrefix { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }


    }
}
