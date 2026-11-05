using AttendanceSystem.DTOs.Attendance;
using AttendanceSystem.Models;

namespace AttendanceSystem.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<List<AttendanceResponseDto>> GetAllAsync();

        Task<AttendanceResponseDto?> GetByIdAsync(int id);

        Task<Attendance> CreateAsync(CreateAttendanceDto dto, int teacherId);

        Task<bool> UpdateAsync(int id, UpdateAttendanceDto dto, int teacherId);

        Task<bool> DeleteAsync(int id);

        Task<List<AttendanceHistoryDto>> GetStudentHistoryAsync(int studentId);


        Task<AttendanceReportDto?> GetStudentReportAsync(int studentId);

        Task<byte[]> ExportToExcelAsync();

        Task ImportFromExcelAsync(IFormFile file , int teacherId);
    }
}