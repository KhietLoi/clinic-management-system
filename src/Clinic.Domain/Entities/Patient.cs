using Clinic.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FullName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string? Address { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string? NationalId { get; set; }

        //Navigation
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
