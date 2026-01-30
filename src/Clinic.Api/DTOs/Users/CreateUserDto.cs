using System.ComponentModel.DataAnnotations;

namespace Clinic.Api.DTOs.Users
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "UserName is required")]
        [MaxLength(100)]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(150)]
        public string UserEmail { get; set; } = null!;

      
        public int RoleId { get; set; }

    }
}
