using Azure.Identity;
using Clinic.Api.DTOs.Users;
using Clinic.Api.Services.Interfaces;
using Clinic.Domain.Entities;
using Clinic.Domain.Entities.Enums;
using Clinic.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Clinic.Api.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ClinicDbContext _ClinicDB;
        private readonly IPasswordHasherService _passwordHasherService;

        public UserService(ClinicDbContext dbContext,
            IPasswordHasherService passwordHasher)
        {
            _ClinicDB = dbContext;
            _passwordHasherService = passwordHasher;
        }

        //Generate Password:
        private static string GenerateRandomPassword (int length = 10)
        {
            const string chars =
              "ABCDEFGHJKLMNPQRSTUVWXYZ" +
              "abcdefghijkmnopqrstuvwxyz" +
              "23456789" +
              "!@#$%";

            var bytes = new byte[length];
            RandomNumberGenerator.Fill(bytes);

            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[bytes[i] % chars.Length];
            }

            return new string(result);
        }


        //Create User
        public async Task<int> CreateAsync(CreateUserDto dto)
        {
            //Kiem tra ng dung thong qua email:
            var exists = await _ClinicDB.Users.AnyAsync(x =>
            x.UserEmail == dto.UserEmail);

            if (exists)
            {
                throw new InvalidOperationException("Email already exists");
            }
            // 1. Sinh password ngẫu nhiên (chỉ 1 lần)
            var rawPassword = GenerateRandomPassword();

            // 2. Hash password
            var passwordHash = _passwordHasherService.Hash(rawPassword);

            var user = new User
            {
                UserEmail = dto.UserEmail,
                UserName = dto.UserName,
                PasswordHash = passwordHash,
                RoleId = dto.RoleId,
                CreatedAt = DateTime.UtcNow

            };
            _ClinicDB.Users.Add(user);
            await _ClinicDB.SaveChangesAsync();
            return user.UserId; 
        }
        //Xem ds User:
        public async Task<List<UserResponseDto>> GetAllAsync(int? roleId = null)
        {
            var query = _ClinicDB.Users
                .AsNoTracking()
                .AsQueryable();

            //Loc  theo vai tro
            if(roleId.HasValue)
            {
                query = query.Where(x => x.RoleId == roleId.Value);

            }

            return await query
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new UserResponseDto
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    UserEmail = x.UserEmail,
                    Status = x.Status,
                    RoleId = x.RoleId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                    
            }).ToListAsync();
                
        }

        public async Task ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            var user = await _ClinicDB.Users
                .FirstOrDefaultAsync (x => x.UserId == userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            user.PasswordHash = _passwordHasherService.Hash(dto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            await _ClinicDB.SaveChangesAsync();
        }

        
        //Thay doi trang thai cua tai khoan
        public async Task ChangeStatusAsync(int userId, UserStatus userStatus)
        {
            var user = await _ClinicDB.Users
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");

            }

            user.Status = userStatus;
            user.UpdatedAt = DateTime.UtcNow;
            await _ClinicDB.SaveChangesAsync();
        }

    
       

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            return await _ClinicDB.Users
                .AsNoTracking()
                .Where(x => x.UserId == id)
                .Select(x => new UserResponseDto
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    UserEmail = x.UserEmail,
                    Status = x.Status,
                    RoleId = x.RoleId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                })
                .FirstOrDefaultAsync();
        }

       /* public Task<UserResponseDto> GetProfileAsync(int userId)
        {
            throw new NotImplementedException();
        }*/

        public async Task UpdateAsync(int userId, UpdateUserDto dto)
        {
            var user = await _ClinicDB.Users
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            var emailExists = await _ClinicDB.Users
                .AnyAsync(x =>
                x.UserEmail == dto.UserEmail &&
                x.UserId != userId);
            if (emailExists) {
                throw new InvalidOperationException("Email already exists");
            }

            //Cap nhat:
            user.UserName = dto.UserName.Trim();
            user.UserEmail = dto.UserEmail.Trim();
            user.UpdatedAt = DateTime.UtcNow;

            await _ClinicDB.SaveChangesAsync();

    
        }
    }
}
