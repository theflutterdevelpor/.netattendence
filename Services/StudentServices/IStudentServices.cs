using AttendanceSystem.DTOs.Student;
using AttendanceSystem.Models;

namespace AttendanceSystem.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student> CreateAsync(CreateStudentDto dto);
        Task<bool> UpdateAsync(int id, UpdateStudentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
