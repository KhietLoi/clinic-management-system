using Clinic.Api.DTOs.Specialties;
using Clinic.Api.Services.Interfaces;
using Clinic.Domain.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Route("api/v1/specialties")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;

        public SpecialtiesController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;

        }


        //GET: api/v1/specialties? keyword = tim:

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword)
        {
            var result = await _specialtyService.GetAllAsync(keyword);
            return Ok(result);
        }

        //GET: api/v1/specialties/5

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _specialtyService.GetByIdAsync(id);

            if (result == null) { return NotFound(); }

            return Ok(result);
        }


        //POST: api/v1/specialties
        [HttpPost]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> Create([FromBody] UpsertSpecialtyDto dto)
        {
            try
            {
                var id = await _specialtyService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new {id},null);

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new {message = ex.Message});

            }

        }

        //Change status:
        //PATCH: api/v1/specialties/5/status?status = 0;
        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromQuery] SpecialtyStatus status)
        {
            try
            {
                var success = await _specialtyService.ChangeStatusAsync(id, status);
                if(!success) return NotFound();

                return NoContent();

            }catch(InvalidOperationException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }


        //DELETE: api/specialties/5

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _specialtyService.DeleteAsync(id);
                if(!success) return NotFound();
                return NoContent();
            }catch(InvalidOperationException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
    }
}
