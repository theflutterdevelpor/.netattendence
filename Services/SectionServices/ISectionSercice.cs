using AttendanceSystem.DTOs.Section;
using AttendanceSystem.Models;

namespace AttendanceSystem.Services.Interfaces
{
    public interface ISectionService
    {
        Task<List<Section>> GetAllAsync();

        Task<Section?> GetByIdAsync(int id);

        Task<Section> CreateAsync(CreateSectionDto dto);

        Task<bool> UpdateAsync(int id, UpdateSectionDto dto);

        Task<bool> DeleteAsync(int id);
    }
}