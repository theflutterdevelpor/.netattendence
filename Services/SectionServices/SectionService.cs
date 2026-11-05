using AttendanceSystem.Database;
using AttendanceSystem.DTOs.Section;
using AttendanceSystem.Models;
using AttendanceSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Services
{
    public class SectionService : ISectionService
    {
        private readonly AppDbContext _context;

        public SectionService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<List<Section>> GetAllAsync()
        {
            return await _context.Sections
                .ToListAsync();
        }

        // GET BY ID
        public async Task<Section?> GetByIdAsync(int id)
        {
            return await _context.Sections
                .FindAsync(id);
        }

        // CREATE
        public async Task<Section> CreateAsync(CreateSectionDto dto)
        {
            var section = new Section
            {
                Name = dto.Name,
                ClassId = dto.ClassId
            };

            _context.Sections.Add(section);

            await _context.SaveChangesAsync();

            return section;
        }

        // UPDATE
        public async Task<bool> UpdateAsync(
            int id,
            UpdateSectionDto dto)
        {
            var existingSection = await _context.Sections
                .FindAsync(id);

            if (existingSection == null)
                return false;

            existingSection.Name = dto.Name;
            existingSection.ClassId = dto.ClassId;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var existingSection = await _context.Sections
                .FindAsync(id);

            if (existingSection == null)
                return false;

            _context.Sections.Remove(existingSection);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}