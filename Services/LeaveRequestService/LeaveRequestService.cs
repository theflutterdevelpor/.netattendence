using AttendanceSystem.Database;
using AttendanceSystem.DTOs.LeaveRequest;
using AttendanceSystem.Models;
using AttendanceSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly AppDbContext _context;

        public LeaveRequestService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<List<LeaveRequestResponseDto>> GetAllAsync()
        {
            return await _context.LeaveRequests
                .Select(l => new LeaveRequestResponseDto
                {
                    Id = l.Id,

                    StudentId = l.StudentId,

                    StudentName = _context.Students
                        .Where(s => s.Id == l.StudentId)
                        .Select(s => s.Name)
                        .FirstOrDefault() ?? "",

                    FromDate = l.FromDate,

                    ToDate = l.ToDate,

                    Reason = l.Reason,

                    Status = l.Status,

                    CreatedAt = l.CreatedAt
                })
                .ToListAsync();
        }

        // GET BY ID
        public async Task<LeaveRequestResponseDto?> GetByIdAsync(int id)
        {
            return await _context.LeaveRequests
                .Where(l => l.Id == id)
                .Select(l => new LeaveRequestResponseDto
                {
                    Id = l.Id,

                    StudentId = l.StudentId,

                    StudentName = _context.Students
                        .Where(s => s.Id == l.StudentId)
                        .Select(s => s.Name)
                        .FirstOrDefault() ?? "",

                    FromDate = l.FromDate,

                    ToDate = l.ToDate,

                    Reason = l.Reason,

                    Status = l.Status,

                    CreatedAt = l.CreatedAt
                })
                .FirstOrDefaultAsync();
        }

        // CREATE
        public async Task<LeaveRequestResponseDto> CreateAsync(
            CreateLeaveRequestDto dto)
        {
            var studentExists = await _context.Students
                .AnyAsync(s => s.Id == dto.StudentId);

            if (!studentExists)
                throw new Exception("Student not found.");

            if (dto.FromDate > dto.ToDate)
                throw new Exception(
                    "FromDate cannot be greater than ToDate.");

            var leaveRequest = new LeaveRequest
            {
                StudentId = dto.StudentId,

                FromDate = dto.FromDate,

                ToDate = dto.ToDate,

                Reason = dto.Reason,

                Status = "Pending",

                CreatedAt = DateTime.Now
            };

            _context.LeaveRequests.Add(leaveRequest);

            await _context.SaveChangesAsync();

            return new LeaveRequestResponseDto
            {
                Id = leaveRequest.Id,

                StudentId = leaveRequest.StudentId,

                StudentName = await _context.Students
                    .Where(s => s.Id == leaveRequest.StudentId)
                    .Select(s => s.Name)
                    .FirstOrDefaultAsync() ?? "",

                FromDate = leaveRequest.FromDate,

                ToDate = leaveRequest.ToDate,

                Reason = leaveRequest.Reason,

                Status = leaveRequest.Status,

                CreatedAt = leaveRequest.CreatedAt
            };
        }

        // UPDATE
        public async Task<bool> UpdateAsync(
            int id,
            UpdateLeaveRequestDto dto)
        {
            var leaveRequest = await _context.LeaveRequests
                .FindAsync(id);

            if (leaveRequest == null)
                return false;

            if (dto.FromDate > dto.ToDate)
                throw new Exception(
                    "FromDate cannot be greater than ToDate.");

            var allowedStatuses = new[]
            {
                "Pending",
                "Approved",
                "Rejected"
            };

            if (!allowedStatuses.Contains(
                dto.Status,
                StringComparer.OrdinalIgnoreCase))
            {
                throw new Exception(
                    "Invalid status. Use Pending, Approved or Rejected.");
            }

            leaveRequest.FromDate = dto.FromDate;

            leaveRequest.ToDate = dto.ToDate;

            leaveRequest.Reason = dto.Reason;

            leaveRequest.Status = dto.Status;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var leaveRequest = await _context.LeaveRequests
                .FindAsync(id);

            if (leaveRequest == null)
                return false;

            _context.LeaveRequests.Remove(leaveRequest);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}