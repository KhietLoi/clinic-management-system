using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities.Enums
{
    public enum  AppointmentStatus:byte
    {
        Pending = 0, //Moi dat
        CheckedIn = 1, //Benh nhan da den
        InProgress = 2, //Dang kham
        Completed = 3, // Kham xong
        Cancelled = 4, //Huy
        NoShow = 5, //Khong toi
    }
}
