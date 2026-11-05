using AttendanceSystem.DTOs.LeaveRequest;
using AttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestsController(
            ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        // GET: api/LeaveRequests
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet]
        public async Task<IActionResult> GetLeaveRequests()
        {
            var leaves =
                await _leaveRequestService.GetAllAsync();

            return Ok(leaves);
        }

        // GET: api/LeaveRequests/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveRequest(int id)
        {
            var leave =
                await _leaveRequestService.GetByIdAsync(id);

            if (leave == null)
                return NotFound();

            return Ok(leave);
        }

        // POST: api/LeaveRequests
        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<IActionResult> CreateLeaveRequest(
            CreateLeaveRequestDto dto)
        {
            var leave =
                await _leaveRequestService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetLeaveRequest),
                new { id = leave.Id },
                leave
            );
        }

        // PUT: api/LeaveRequests/1
        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeaveRequest(
            int id,
            UpdateLeaveRequestDto dto)
        {
            var updated =
                await _leaveRequestService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/LeaveRequests/1
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            var deleted =
                await _leaveRequestService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}