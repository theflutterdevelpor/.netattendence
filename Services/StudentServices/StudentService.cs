using AttendanceSystem.Database;
using AttendanceSystem.DTOs.Student;
using AttendanceSystem.Models;
using AttendanceSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students
                .ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students
                .FindAsync(id);
        }

        public async Task<Student> CreateAsync(CreateStudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                FatherName = dto.FatherName,
                RollNumber = dto.RollNumber,
                Email = dto.Email,
                Phone = dto.Phone,
                ClassId = dto.ClassId,
                SectionId = dto.SectionId,
                CreatedAt = DateTime.Now
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<bool> UpdateAsync(
         int id,
         UpdateStudentDto dto)
        {
            var existingStudent = await _context.Students
                .FindAsync(id);

            if (existingStudent == null)
                return false;

            existingStudent.Name = dto.Name;
            existingStudent.FatherName = dto.FatherName;
            existingStudent.RollNumber = dto.RollNumber;
            existingStudent.Email = dto.Email;
            existingStudent.Phone = dto.Phone;
            existingStudent.ClassId = dto.ClassId;
            existingStudent.SectionId = dto.SectionId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students
                .FindAsync(id);

            if (student == null)
                return false;

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
