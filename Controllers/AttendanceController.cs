using AttendanceSystem.DTOs.Attendance;
using AttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AttendanceSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendancesController(
            IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        // GET: api/Attendances
        [HttpGet]
        public async Task<IActionResult> GetAttendances()
        {
            var attendances =
                await _attendanceService.GetAllAsync();

            return Ok(attendances);
        }

        // GET: api/Attendances/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendance(int id)
        {
            var attendance =
                await _attendanceService.GetByIdAsync(id);

            if (attendance == null)
                return NotFound();

            return Ok(attendance);
        }

        // POST: api/Attendances
        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<IActionResult> CreateAttendance(
      CreateAttendanceDto dto)
        {
            var teacherIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (teacherIdClaim == null)
                return Unauthorized();

            int teacherId = int.Parse(teacherIdClaim.Value);

            var attendance =
                await _attendanceService.CreateAsync(dto, teacherId);

            return Ok(attendance);
        }

        // PUT: api/Attendances/1
        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendance(
            int id,
            UpdateAttendanceDto dto
            )
        {

            var teacherIdClaim =
       User.FindFirst(ClaimTypes.NameIdentifier);

            if (teacherIdClaim == null)
                return Unauthorized();

            int teacherId = int.Parse(teacherIdClaim.Value);
            var updated =
                await _attendanceService.UpdateAsync(id, dto,teacherId );

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Attendances/1
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var deleted =
                await _attendanceService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }



        // GET: api/Attendances/student/1
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentHistory(int studentId)
        {
            var history =
                await _attendanceService.GetStudentHistoryAsync(studentId);

            return Ok(history);
        }


        // GET: api/Attendances/report/student/1
        [HttpGet("report/student/{studentId}")]
        public async Task<IActionResult> GetStudentReport(int studentId)
        {
            var report =
                await _attendanceService.GetStudentReportAsync(studentId);

            if (report == null)
                return NotFound();

            return Ok(report);
        }


        [HttpGet("export")]
        public async Task<IActionResult> ExportAttendance()
        {
            var file =
                await _attendanceService.ExportToExcelAsync();

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Attendance.xlsx"
            );
        }



        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost("import")]
        public async Task<IActionResult> ImportAttendance(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload an Excel file.");

            var teacherIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier);

            if (teacherIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(teacherIdClaim.Value, out int teacherId))
                return Unauthorized();

            await _attendanceService.ImportFromExcelAsync(
                file,
                teacherId);

            return Ok(new
            {
                message = "Attendance imported successfully."
            });
        }

    }
}