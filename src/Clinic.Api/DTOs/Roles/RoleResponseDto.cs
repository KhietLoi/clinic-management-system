using System.ComponentModel.DataAnnotations;

namespace Clinic.Api.DTOs.Roles
{
    public class RoleResponseDto
    {
        public int RoleId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public int UserCount { get; set; }
    }
}
