using Clinic.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Api.DTOs.ClinicRooms
{
    public class CreateClinicRoomDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        //Location:
        [MaxLength(200)]
        public string? Location { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public int SpecialtyId { get; set; } //Gắn với chuyên khoa

    }
}
