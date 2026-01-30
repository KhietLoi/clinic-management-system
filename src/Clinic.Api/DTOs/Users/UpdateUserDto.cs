using System.ComponentModel.DataAnnotations;

namespace Clinic.Api.DTOs.Users
{
    public class UpdateUserDto
    {
        [Required, StringLength(100)]
        public string UserName { get; set; } = null!;

        [Required, EmailAddress, StringLength(150)]
        public string UserEmail { get; set; } = null!;

      
    }
}
