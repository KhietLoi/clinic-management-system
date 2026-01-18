using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string Name { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation:
        public ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();

    }
}
