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
        public string? Location { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //Navigation
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
