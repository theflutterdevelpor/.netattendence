using AttendanceSystem.DTOs.Subject;
using AttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService) 
        {
            _subjectService = subjectService;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _subjectService.GetAllAsync();

            return Ok(subjects);
        }

        // GET: api/Subjects/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            var subject = await _subjectService.GetByIdAsync(id);

            if (subject == null)
                return NotFound();

            return Ok(subject);
        }

        // POST: api/Subjects
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> CreateSubject(
            CreateSubjectDto dto)
        {
            var createdSubject =
                await _subjectService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetSubject),
                new { id = createdSubject.Id },
                createdSubject
            );
        }

        // PUT: api/Subjects/1
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateSubject(
            int id,
            UpdateSubjectDto dto)
        {
            var updated =
                await _subjectService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Subjects/1
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteSubject(int id)
        {
            var deleted =
                await _subjectService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}