using Clinic.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities
{
    public class ClinicRoom
    {
        public int ClinicRoomId { get; set; }
        public string Name { get; set; } = null!;

        //Thêm thuộc tính mã phòng:
        public string RoomCode { get; set; } = null!;
        //Thêm thuộc tính trạng thái phòng:
        public ClinicRoomStatus Status { get; set; } = ClinicRoomStatus.Active;
        public string? Location { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //n-1
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; } = null!;

        //Navigation
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
