using Clinic.Api.DTOs.Roles;

namespace Clinic.Api.Services.Interfaces
{
    public interface IRoleService
    {
        Task<int> CreateAsync(CreateRoleDto dto);
        Task  UpdateAsync(int roleId, UpdateRoleDto dto);
        Task DeleteAsync(int roleId);


        Task<RoleResponseDto> GetByIdAsync(int roleId);
        Task<IReadOnlyList<RoleResponseDto>> GetAllAsync();

    }
}
