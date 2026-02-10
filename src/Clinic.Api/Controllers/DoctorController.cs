using Clinic.Api.DTOs.Doctors;
using Clinic.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }


        //GET: api/v1/doctors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _doctorService.GetAllAsync();
            return Ok(doctors);
        }

        //GET: api/v1/doctors/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById (int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        //POST: api/v1/doctors
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDoctordto dto)
        {
            var id = await _doctorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        //PUT: api/v1/doctors/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update (int id, [FromBody] UpdateDoctorDto dto)
        {
            await _doctorService.UpdateAsync(id, dto);
            return Ok(dto);
        }


        //DELETE: api/v1/doctors/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete (int id)
        {
            await _doctorService.DeleteAsync(id);
            return NoContent();
        }

    }
}
