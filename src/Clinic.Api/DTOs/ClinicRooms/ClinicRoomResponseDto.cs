using Clinic.Domain.Entities.Enums;

namespace Clinic.Api.DTOs.ClinicRooms
{
    public class ClinicRoomResponseDto
    {
        public int ClinicRoomId { get; set; }

        public string RoomCode { get; set; } = null!;
        public string Name { get; set; } = null!;

        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set;} = null!;

        public ClinicRoomStatus Status { get; set; }

        public string? Location { get; set; }
        public string? Description { get; set; }

    }
}
