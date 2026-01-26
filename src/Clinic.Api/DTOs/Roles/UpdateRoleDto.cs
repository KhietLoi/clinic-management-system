using System.ComponentModel.DataAnnotations;

namespace Clinic.Api.DTOs.Roles
{
    public class UpdateRoleDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
