using AttendanceSystem.Database;
using AttendanceSystem.DTOs.Subject;
using AttendanceSystem.Models;
using AttendanceSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly AppDbContext _context;

        public SubjectService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<List<Subject>> GetAllAsync()
        {
            return await _context.Subjects
                .ToListAsync();
        }

        // GET BY ID
        public async Task<Subject?> GetByIdAsync(int id)
        {
            return await _context.Subjects
                .FindAsync(id);
        }

        // CREATE
        public async Task<Subject> CreateAsync(CreateSubjectDto dto)
        {
            var subject = new Subject
            {
                Name = dto.Name,
                ClassId = dto.ClassId
            };

            _context.Subjects.Add(subject);

            await _context.SaveChangesAsync();

            return subject;
        }

        // UPDATE
        public async Task<bool> UpdateAsync(
            int id,
            UpdateSubjectDto dto)
        {
            var existingSubject = await _context.Subjects
                .FindAsync(id);

            if (existingSubject == null)
                return false;

            existingSubject.Name = dto.Name;
            existingSubject.ClassId = dto.ClassId;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var existingSubject = await _context.Subjects
                .FindAsync(id);

            if (existingSubject == null)
                return false;

            _context.Subjects.Remove(existingSubject);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}