using Clinic.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Api.DTOs.Doctors
{
    public class CreateDoctordto
    {

        [Required,MaxLength(200)]
        public string FullName { get; set; } = null!;
        
        [Required,MaxLength(20)]
        [Phone]
        public string Phone { get; set; } = null!;
        
        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Range(0,60)]
        public int YearOfExperience { get; set; }

        [Required]
        public DateOnly DateOfBỉth {  get; set; }

        [Required]
        public Gender Gender { get; set; }


        //CHUA CAN XD CHUYEN NGANH:
        public int? SpecialtyId { get; set; }

        [Required]
        public int UserId { get; set; }

        
    }
}
