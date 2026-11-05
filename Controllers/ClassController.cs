using AttendanceSystem.DTOs.Class;
using AttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<IActionResult> GetClasses()
        {
            var classes = await _classService.GetAllAsync();

            return Ok(classes);
        }

        // GET: api/Classes/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClass(int id)
        {
            var classEntity = await _classService.GetByIdAsync(id);

            if (classEntity == null)
                return NotFound();

            return Ok(classEntity);
        }

        // POST: api/Classes
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> CreateClass(
            CreateClassDto dto)
        {
            var createdClass = await _classService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetClass),
                new { id = createdClass.Id },
                createdClass
            );
        }

        // PUT: api/Classes/1
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateClass(
            int id,
            UpdateClassDto dto)
        {
            var updated = await _classService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Classes/1
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteClass(int id)
        {
            var deleted = await _classService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}