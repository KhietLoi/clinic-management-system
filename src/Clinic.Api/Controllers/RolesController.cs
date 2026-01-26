using Clinic.Api.DTOs.Roles;
using Clinic.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        //cau hinh sau:

        //Create Role
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDto dto)
        {
            var id = await _roleService.CreateAsync(dto);
            return Ok(new { RoleId = id });

        }


        //Update role:
        [HttpPut("{roleId:int}")]
        public async Task<IActionResult> Update(int roleId, UpdateRoleDto dto)
        {
            await _roleService.UpdateAsync(roleId, dto);
            return NoContent();

        }

        //Delete Role:
        [HttpDelete("{roleId:int}")]
        public async Task<IActionResult> Delete(int roleId)
        {
            await _roleService.DeleteAsync(roleId);
            return NoContent();
        }

        //Get by Id:
        [HttpGet("{roleId:int}")]
        public async Task<IActionResult> GetById (int roleId)
        {
            var result = await _roleService.GetByIdAsync(roleId);
            return Ok(result);
        }

        //Gel All Roles:
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleService.GetAllAsync();
            return Ok(result);
        }

    }
}
