using Clinic.Api.DTOs.Patients;
using Clinic.Api.DTOs.Roles;
using Clinic.Api.Services.Interfaces;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Api.Services.Implementations
{
    public class RoleService : IRoleService
    {

        private readonly ClinicDbContext _clinicDb;

        public RoleService (ClinicDbContext clinicDb)
        {
            _clinicDb = clinicDb;
        }
        public async Task<int> CreateAsync(CreateRoleDto dto)
        {
            var name = dto.Name.Trim();

            var exists = await _clinicDb.Roles
                .AnyAsync(r => r.Name.ToLower() == name.ToLower());
            if (exists)
            {
                throw new InvalidOperationException("Role name already exists.");
            }
            var entity = new Role
            {
                Name = name,
                Description = dto.Description?.Trim()
            };

            _clinicDb.Add(entity);
            await _clinicDb.SaveChangesAsync();

            return entity.RoleId;

        }

        public async  Task DeleteAsync(int roleId)
        {
            var roleExists = await _clinicDb.Roles
                .AsNoTracking()
                .AnyAsync(x =>x.RoleId == roleId);

            if(!roleExists)
            {
                throw new KeyNotFoundException("Role not found");
                
            }

            var isUsed = await _clinicDb.Users
                .AsNoTracking()
                .AnyAsync(X => X.RoleId == roleId);

            if(isUsed)
            {
                throw new InvalidOperationException("Cannot delete role because it is being used by users.");
            }

            var role = new Role { RoleId = roleId };
            _clinicDb.Roles.Attach(role);
            _clinicDb.Roles.Remove(role);
            
            await _clinicDb.SaveChangesAsync();
            
        }

        public async Task<IReadOnlyList<RoleResponseDto>> GetAllAsync()
        {
            {
                return await _clinicDb.Roles
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .Select(x => new RoleResponseDto
                    {
                        RoleId = x.RoleId,
                        Name = x.Name,
                        Description = x.Description,
                        UserCount = x.Users.Count()
                    })
                    .ToListAsync();
            }
        }

            

            //Loc role
            public async  Task<RoleResponseDto> GetByIdAsync(int roleId)
        {
            var role = await _clinicDb.Roles.AsNoTracking()
                .Where(x => x.RoleId == roleId)
                .Select(x => new RoleResponseDto
                {
                    RoleId = x.RoleId,
                    Name = x.Name,
                    Description = x.Description,
                    UserCount = x.Users.Count()
                }).FirstOrDefaultAsync();

            if (role == null)
            {
                throw new InvalidOperationException("Role not found");
            }

            return role;
        }

        //Cap nhat 1 role cu the 
        public async Task UpdateAsync(int roleId, UpdateRoleDto dto)
        {
            var role = await _clinicDb.Roles
                .FirstOrDefaultAsync(x => x.RoleId == roleId);

            if (role == null)
            {
                throw new KeyNotFoundException("Role not found");
            }

            var name = dto.Name.Trim();

            var exists = await _clinicDb.Roles
                .AnyAsync(x => x.Name.ToLower() == name.ToLower() && x.RoleId != roleId );

            if (exists)
            {
                throw new InvalidOperationException("Role name already exists.");
            }

            role.Name = name;
            role.Description = dto.Description?.Trim();

            await _clinicDb.SaveChangesAsync();
            

        }

    
    }
}
