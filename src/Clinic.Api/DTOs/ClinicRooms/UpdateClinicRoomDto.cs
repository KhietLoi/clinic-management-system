namespace Clinic.Api.DTOs.ClinicRooms
{
    public class UpdateClinicRoomDto
    {
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
        public string? Description { get; set; }

    }
}
