using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Notes { get; set; }
        //FK
        public int MedicalRecordId { get; set; }

        //Navigation
        public MedicalRecord MedicalRecord { get; set; } = null!;
        public ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
    }
}
