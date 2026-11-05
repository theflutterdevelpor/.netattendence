using AttendanceSystem.Database;
using AttendanceSystem.DTOs.Class;
using AttendanceSystem.Models;
using AttendanceSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Services
{
    public class ClassService : IClassService
    {
        private readonly AppDbContext _context;

        public ClassService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<List<Class>> GetAllAsync()
        {
            return await _context.Classes
                .ToListAsync();
        }

        // GET BY ID
        public async Task<Class?> GetByIdAsync(int id)
        {
            return await _context.Classes
                .FindAsync(id);
        }

        // CREATE
        public async Task<Class> CreateAsync(CreateClassDto dto)
        {
            var classEntity = new Class
            {
                Name = dto.Name
            };

            _context.Classes.Add(classEntity);

            await _context.SaveChangesAsync();

            return classEntity;
        }

        // UPDATE
        public async Task<bool> UpdateAsync(
            int id,
            UpdateClassDto dto)
        {
            var existingClass = await _context.Classes
                .FindAsync(id);

            if (existingClass == null)
                return false;

            existingClass.Name = dto.Name;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var existingClass = await _context.Classes
                .FindAsync(id);

            if (existingClass == null)
                return false;

            _context.Classes.Remove(existingClass);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
