using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Domain.Entities.Enums;



namespace Clinic.Domain.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime ScheduledAt { get; set; } //Giờ hẹn dự kiến

        public DateTime? CheckInAt { get; set; } // Bệnh nhân đến 


        public AppointmentStatus Status { get; set; }           // enum
        public string? Reason { get; set; }
        // Walk-in: bệnh nhân tới trực tiếp không đặt lịch trước
        public bool IsWalkIn { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //FK
        public int PatientId { get; set; }
        public int ClinicRoomId { get; set; }
        public int DoctorId { get; set; }



        //Navigation
        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        public ClinicRoom ClinicRoom { get; set; } = null!;

        public MedicalRecord? MedicalRecord { get; set; }

    }

}
