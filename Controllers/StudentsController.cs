using AttendanceSystem.DTOs.Student;
using AttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetAllAsync();

            return Ok(students);
        }

        // GET: api/Students/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetByIdAsync(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST: api/Students
        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]

        public async Task<IActionResult> CreateStudent(
            CreateStudentDto dto)
        {
            var createdStudent = await _studentService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = createdStudent.Id },
                createdStudent
            );
        }

        // PUT: api/Students/1
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Teacher")]

        public async Task<IActionResult> UpdateStudent(
            int id,
            UpdateStudentDto dto)
        {
            var updated = await _studentService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Students/1
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteStudent(int id)
        {
            var deleted = await _studentService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}