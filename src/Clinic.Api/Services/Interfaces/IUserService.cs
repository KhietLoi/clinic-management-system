using Clinic.Api.DTOs.Users;
using Clinic.Domain.Entities;
using Clinic.Domain.Entities.Enums;

namespace Clinic.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync(int? roleId = null); //Tra ve danh sach
        Task<UserResponseDto?> GetByIdAsync(int id); //Tra ve mot ng dung cu the

        //ADMIN TAO tk
        Task <int> CreateAsync (CreateUserDto dto);
        //Cap nhat tai khoan nguoi dung:

        Task UpdateAsync (int userId, UpdateUserDto dto);
        Task ChangeStatusAsync (int userId, UserStatus userStatus);

        Task ChangePasswordAsync (int userId, ChangePasswordDto dto);


   /*     //Xem profile
        Task<UserResponseDto> GetProfileAsync(int userId);*/



    }
}
