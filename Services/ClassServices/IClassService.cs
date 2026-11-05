using AttendanceSystem.DTOs.Class;
using AttendanceSystem.Models;

namespace AttendanceSystem.Services.Interfaces
{
    public interface IClassService
    {
        Task<List<Class>> GetAllAsync();

        Task<Class?> GetByIdAsync(int id);

        Task<Class> CreateAsync(CreateClassDto dto);

        Task<bool> UpdateAsync(int id, UpdateClassDto dto);

        Task<bool> DeleteAsync(int id);
    }
}