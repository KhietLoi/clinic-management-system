using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities.Enums
{
    public enum UserStatus
    {
        PendingVerification = 0, //Đã tạo tài khoản nhưng chưa xác thực
        Active = 1, //Đã xác thực
        TemporarilyInactive = 2, //Tạm vắng (Nghỉ phép)
        Terminated = 3 //Nghỉ việc
    }
}
