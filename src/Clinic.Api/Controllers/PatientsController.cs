using Clinic.Api.DTOs.Patients;
using Clinic.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Route("api/v1/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList(
        [FromQuery] string? keyword,
        [FromQuery] string? phone,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
        {
            var (items, total) = await _patientService.GetListAsync(keyword, phone, page, pageSize);
            return Ok(new { items, total, page, pageSize });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);

        }

        [HttpPost]
        public async Task<IActionResult> Create (CreatePatientDto dto)
        {
            try
            {
                var id = await _patientService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id }, new {id});
            }catch(InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });

            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdatePatientDto dto)
        {
            try
            {
                var ok = await _patientService.UpdateAsync(id, dto);
                if (!ok) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ok = await _patientService.DeleteAsync(id);
                if (!ok) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }


    }
}
