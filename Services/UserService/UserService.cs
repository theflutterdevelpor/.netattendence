using AttendanceSystem.Database;
using AttendanceSystem.DTOs.User;
using AttendanceSystem.Models;
using AttendanceSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            return await _context.Users
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();
        }

        // GET BY ID
        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt
                })
                .FirstOrDefaultAsync();
        }

        // CREATE
        public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
        {
            var allowedRoles = new[]
            {
        "Admin",
        "Teacher",
        "Student"
    };

            if (!allowedRoles.Contains(
                dto.Role,
                StringComparer.OrdinalIgnoreCase))
            {
                throw new Exception(
                    "Invalid role. Use Admin, Teacher or Student.");
            }

            var emailExists = await _context.Users
                .AnyAsync(u => u.Email == dto.Email);

            if (emailExists)
            {
                throw new Exception(
                    "Email already exists.");
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        // UPDATE
        public async Task<bool> UpdateAsync(
            int id,
            UpdateUserDto dto)
        {
            var existingUser = await _context.Users
                .FindAsync(id);

            if (existingUser == null)
                return false;

            existingUser.Name = dto.Name;
            existingUser.Email = dto.Email;
            existingUser.Role = dto.Role;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users
                .FindAsync(id);

            if (user == null)
                return false;

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }



        public async Task<User?> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return null;

            bool passwordValid = BCrypt.Net.BCrypt.Verify(
         dto.Password,
         user.PasswordHash);

            if (!passwordValid)
                return null;

            return user;
        }




    }
}