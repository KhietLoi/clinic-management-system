using Clinic.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!; //Thêm email để dùng mailkit

        public string PasswordHash { get; set; } = null!;
        public UserStatus Status { get; set; } = UserStatus.PendingVerification;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public Doctor? Doctor { get; set; }
    }
}
