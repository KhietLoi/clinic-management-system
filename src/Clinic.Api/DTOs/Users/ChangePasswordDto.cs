using System.ComponentModel.DataAnnotations;

namespace Clinic.Api.DTOs.Users
{
    public class ChangePasswordDto
    {
        [Required]
        public string OldPassword { get; set; } = null!;

        [Required, MinLength(6)]
        public string NewPassword { get; set; } = null!;
    }
}
