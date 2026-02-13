using Clinic.Api.DTOs.ClinicRooms;
using Clinic.Api.Services.Implementations;
using Clinic.Api.Services.Interfaces;
using Clinic.Domain.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClinicRoomsController : ControllerBase
    {
        private readonly IClinicRoomService _clinicRoomService;

        public ClinicRoomsController(IClinicRoomService clinicRoomService)
        {
            _clinicRoomService = clinicRoomService;
        }

        //GET: api/v1/clinicrooms
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clinicrooms = await _clinicRoomService.GetAllAsync();
            return Ok(clinicrooms);
        }

        //GET: api/v1/clinicrooms/id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var clinicroom = await _clinicRoomService.GetByIdAsync(id);
            if (clinicroom == null)
            {
                return NotFound();
            }
            return Ok(clinicroom);
        }

        //POST: api/v1/clinicrooms
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClinicRoomDto clinicRoomDto)
        {
            var id = await _clinicRoomService.CreateAsync(clinicRoomDto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        //PUT: api/v1/clinicrooms/id
        [HttpPut("{id:int}")]
        public async Task<IActionResult>Update (int id, [FromBody] UpdateClinicRoomDto updateClinicRoomDto)
        {
            await _clinicRoomService.UpdateAsync(id, updateClinicRoomDto);
            return Ok();
        }

        //DELETE: api/v1/clinicrooms/id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete (int id)
        {
            await _clinicRoomService.DeleteAsync(id);
            return Ok();
        }


        //Change status ClinicRoom: PATCH: api/v1/clinicrooms/id/status?status = 0
        // Change status:
        // PATCH: api/v1/specialties/5/status?status=0
        [HttpPatch("{id:int}/status")]

        public async Task<IActionResult> ChangeStatus(int id, [FromQuery] ClinicRoomStatus status)
        {
            await _clinicRoomService.ChangeClinicRoomStatusAsync(id, status);
            return Ok();
        }
    }
}
