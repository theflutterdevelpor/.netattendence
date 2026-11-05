using AttendanceSystem.DTOs.Section;
using AttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionsController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        // GET: api/Sections
        [HttpGet]
        public async Task<IActionResult> GetSections()
        {
            var sections = await _sectionService.GetAllAsync();

            return Ok(sections);
        }

        // GET: api/Sections/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSection(int id)
        {
            var section = await _sectionService.GetByIdAsync(id);

            if (section == null)
                return NotFound();

            return Ok(section);
        }

        // POST: api/Sections
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> CreateSection(
            CreateSectionDto dto)
        {
            var createdSection =
                await _sectionService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetSection),
                new { id = createdSection.Id },
                createdSection
            );
        }

        // PUT: api/Sections/1
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateSection(
            int id,
            UpdateSectionDto dto)
        {
            var updated =
                await _sectionService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Sections/1
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteSection(int id)
        {
            var deleted =
                await _sectionService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}