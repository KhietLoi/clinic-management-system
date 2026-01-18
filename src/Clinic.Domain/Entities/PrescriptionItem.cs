using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities
{
    public class PrescriptionItem
    {
        public int PrescriptionItemId { get; set; }

        public string Dosage { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Instructions { get; set; }
        public string? Notes { get; set; }

        //FK
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }

        //Navigation
        public Prescription Prescription { get; set; } = null!;
        public Medicine Medicine { get; set; } = null!;
    }
}
