using Clinic.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } = null;

        public SpecialtyStatus IsActive { get; set; } = SpecialtyStatus.Active;

        //Navigation
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
        public ClinicRoom? ClinicRoom { get; set; }



    }
}
