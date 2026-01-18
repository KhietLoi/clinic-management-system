using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities
{
    public class MedicalRecord
    {
        public int MedicalRecordId { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime? StartAt { get; set; } // Bắt đầu khám, kết thúc khám
        public DateTime? EndAt {  get; set; } //Kết thúc khám
        public string? Symptomps { get; set; }

        public string? Diagnosis { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //FK

        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }

        //Navigation
        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        public Appointment Appointment { get; set; } = null!;

        public Prescription Prescription { get; set; } = null!;
    }
}
