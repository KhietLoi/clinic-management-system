using Clinic.Api.DTOs.Users;
using Clinic.Api.Services.Interfaces;
using Clinic.Domain.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        //GET: api/v1/users? roleId =..
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? roleId)
        {
            var users = await _userService.GetAllAsync(roleId);
            return Ok(users);

        }

        // GET: api/v1/users/{id}
        // =========================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        // POST:api/v1/users
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var userId = await _userService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = userId }, null);
        }

        //PUT:api/v1/users/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update (int id, [FromBody] UpdateUserDto dto)
        {
            await _userService.UpdateAsync(id, dto);
            return NoContent();
        }

        //PATCH:api/v1/users/{id}/status
        // =========================
        [HttpPatch("{id:int}/status")]
        public async Task <IActionResult> ChangeStatus
            (int id, [FromQuery] UserStatus status)
        {
            await _userService.ChangeStatusAsync(id,status);
            return NoContent();
        }

        //PATCH:api/v1/users/{id}/
        // =========================
        [HttpPatch("{id:int}/password")]
        public async Task<IActionResult> ChangePassword
            (int id, [FromBody] ChangePasswordDto dto)
        {
            await _userService.ChangePasswordAsync(id, dto);
            return NoContent();
        }

    }
}
