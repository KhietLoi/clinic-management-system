using Clinic.Domain.Entities.Enums;
using Clinic.Domain.Entities;

namespace Clinic.Api.DTOs.Users
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!; //Thêm email để dùng mailkit

        public UserStatus Status { get; set; } = UserStatus.PendingVerification;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int RoleId { get; set; }

    }
}
