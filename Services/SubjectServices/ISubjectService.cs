using AttendanceSystem.DTOs.Subject;
using AttendanceSystem.Models;

namespace AttendanceSystem.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllAsync();

        Task<Subject?> GetByIdAsync(int id);

        Task<Subject> CreateAsync(CreateSubjectDto dto);

        Task<bool> UpdateAsync(int id, UpdateSubjectDto dto);

        Task<bool> DeleteAsync(int id);
    }
}