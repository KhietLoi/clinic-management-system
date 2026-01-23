using Clinic.Domain.Entities.Enums;

namespace Clinic.Api.DTOs.Specialties
{
    public class SpecialtyResponseDto
    {

        public int SpecialtyId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public SpecialtyStatus IsActive { get; set; }

    }
}
