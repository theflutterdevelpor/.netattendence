using AttendanceSystem.DTOs.LeaveRequest;

namespace AttendanceSystem.Services.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<List<LeaveRequestResponseDto>> GetAllAsync();

        Task<LeaveRequestResponseDto?> GetByIdAsync(int id);

        Task<LeaveRequestResponseDto> CreateAsync(CreateLeaveRequestDto dto);

        Task<bool> UpdateAsync(int id, UpdateLeaveRequestDto dto);

        Task<bool> DeleteAsync(int id);
    }
}