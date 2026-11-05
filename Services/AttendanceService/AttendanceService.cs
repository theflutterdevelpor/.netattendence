using AttendanceSystem.Database;
using AttendanceSystem.DTOs.Attendance;
using AttendanceSystem.Models;
using AttendanceSystem.Services.Interfaces;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly AppDbContext _context;

        public AttendanceService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<List<AttendanceResponseDto>> GetAllAsync()
        {
            var attendances = await _context.Attendances
                .ToListAsync();

            var result = attendances.Select(a => new AttendanceResponseDto
            {
                Id = a.Id,

                StudentName = _context.Students
                    .Where(s => s.Id == a.StudentId)
                    .Select(s => s.Name)
                    .FirstOrDefault() ?? "",

                ClassName = _context.Classes
                    .Where(c => c.Id == a.ClassId)
                    .Select(c => c.Name)
                    .FirstOrDefault() ?? "",

                SectionName = _context.Sections
                    .Where(s => s.Id == a.SectionId)
                    .Select(s => s.Name)
                    .FirstOrDefault() ?? "",

                SubjectName = _context.Subjects
                    .Where(s => s.Id == a.SubjectId)
                    .Select(s => s.Name)
                    .FirstOrDefault() ?? "",

                TeacherId = a.TeacherId,

                Date = a.Date,

                Status = a.Status,

                CheckInTime = a.CheckInTime
            }).ToList();

            return result;
        }

        // GET BY ID
        public async Task<AttendanceResponseDto?> GetByIdAsync(int id)
        {
            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(a => a.Id == id);

            if (attendance == null)
                return null;

            var result = new AttendanceResponseDto
            {
                Id = attendance.Id,

                StudentName = await _context.Students
                    .Where(s => s.Id == attendance.StudentId)
                    .Select(s => s.Name)
                    .FirstOrDefaultAsync() ?? "",

                ClassName = await _context.Classes
                    .Where(c => c.Id == attendance.ClassId)
                    .Select(c => c.Name)
                    .FirstOrDefaultAsync() ?? "",

                SectionName = await _context.Sections
                    .Where(s => s.Id == attendance.SectionId)
                    .Select(s => s.Name)
                    .FirstOrDefaultAsync() ?? "",

                SubjectName = await _context.Subjects
                    .Where(s => s.Id == attendance.SubjectId)
                    .Select(s => s.Name)
                    .FirstOrDefaultAsync() ?? "",

                TeacherId = attendance.TeacherId,

                Date = attendance.Date,

                Status = attendance.Status,

                CheckInTime = attendance.CheckInTime
            };

            return result;
        }

        // CREATE
        public async Task<Attendance> CreateAsync(CreateAttendanceDto dto, int teacherId)
        {
            var allowedStatuses = new[]
            {
        "Present",
        "Absent",
        "Late",
        "Leave"
    };

            if (!allowedStatuses.Contains(dto.Status))
            {
                throw new Exception(
                    "Invalid status. Use Present, Absent, Late or Leave."
                );
            }

            var attendance = new Attendance
            {
                StudentId = dto.StudentId,
                ClassId = dto.ClassId,
                SectionId = dto.SectionId,
                SubjectId = dto.SubjectId,
                TeacherId = teacherId,
                Date = dto.Date,
                Status = dto.Status,
                CheckInTime = dto.CheckInTime,
                CreatedAt = DateTime.Now
            };

            _context.Attendances.Add(attendance);

            await _context.SaveChangesAsync();

            return attendance;
        }

        // UPDATE
        public async Task<bool> UpdateAsync(
            int id,
            UpdateAttendanceDto dto, int teacherId)
        {
            var existingAttendance =
                await _context.Attendances.FindAsync(id);

            if (existingAttendance == null)
                return false;

            if (existingAttendance.TeacherId != teacherId)
                return false;

            existingAttendance.StudentId = dto.StudentId;
            existingAttendance.ClassId = dto.ClassId;
            existingAttendance.SectionId = dto.SectionId;
            existingAttendance.SubjectId = dto.SubjectId;
            existingAttendance.Date = dto.Date;
            existingAttendance.Status = dto.Status;
            existingAttendance.CheckInTime = dto.CheckInTime;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var attendance =
                await _context.Attendances.FindAsync(id);

            if (attendance == null)
                return false;

            _context.Attendances.Remove(attendance);

            await _context.SaveChangesAsync();

            return true;
        }




        // STUDENT ATTENDANCE HISTORY
        public async Task<List<AttendanceHistoryDto>> GetStudentHistoryAsync(
            int studentId)
        {
            return await _context.Attendances
                .Where(a => a.StudentId == studentId)
                .Select(a => new AttendanceHistoryDto
                {
                    Id = a.Id,

                    StudentId = a.StudentId,

                    StudentName = _context.Students
                        .Where(s => s.Id == a.StudentId)
                        .Select(s => s.Name)
                        .FirstOrDefault() ?? "",

                    ClassName = _context.Classes
                        .Where(c => c.Id == a.ClassId)
                        .Select(c => c.Name)
                        .FirstOrDefault() ?? "",

                    SectionName = _context.Sections
                        .Where(s => s.Id == a.SectionId)
                        .Select(s => s.Name)
                        .FirstOrDefault() ?? "",

                    SubjectName = _context.Subjects
                        .Where(s => s.Id == a.SubjectId)
                        .Select(s => s.Name)
                        .FirstOrDefault() ?? "",

                    Date = a.Date,

                    Status = a.Status,

                    CheckInTime = a.CheckInTime
                })
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }


        // STUDENT ATTENDANCE REPORT
        public async Task<AttendanceReportDto?> GetStudentReportAsync(
            int studentId)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
                return null;

            var attendances = await _context.Attendances
                .Where(a => a.StudentId == studentId)
                .ToListAsync();

            var totalDays = attendances.Count;

            var present = attendances
                .Count(a => a.Status == "Present");

            var absent = attendances
                .Count(a => a.Status == "Absent");

            var late = attendances
                .Count(a => a.Status == "Late");

            var leave = attendances
                .Count(a => a.Status == "Leave");

            double percentage = 0;

            if (totalDays > 0)
            {
                percentage =
                    ((double)(present + late) / totalDays) * 100;
            }

            return new AttendanceReportDto
            {
                StudentId = student.Id,

                StudentName = student.Name,

                TotalDays = totalDays,

                Present = present,

                Absent = absent,

                Late = late,

                Leave = leave,

                AttendancePercentage = percentage
            };
        }


        public async Task<byte[]> ExportToExcelAsync()
        {
            var attendances = await _context.Attendances
                .ToListAsync();

            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Attendance");

            worksheet.Cell(1, 1).Value = "Student";
            worksheet.Cell(1, 2).Value = "Class";
            worksheet.Cell(1, 3).Value = "Section";
            worksheet.Cell(1, 4).Value = "Subject";
            worksheet.Cell(1, 5).Value = "Teacher ID";
            worksheet.Cell(1, 6).Value = "Date";
            worksheet.Cell(1, 7).Value = "Status";
            worksheet.Cell(1, 8).Value = "Check In Time";

            var row = 2;

            foreach (var attendance in attendances)
            {
                worksheet.Cell(row, 1).Value =
                    await _context.Students
                        .Where(s => s.Id == attendance.StudentId)
                        .Select(s => s.Name)
                        .FirstOrDefaultAsync() ?? "";

                worksheet.Cell(row, 2).Value =
                    await _context.Classes
                        .Where(c => c.Id == attendance.ClassId)
                        .Select(c => c.Name)
                        .FirstOrDefaultAsync() ?? "";

                worksheet.Cell(row, 3).Value =
                    await _context.Sections
                        .Where(s => s.Id == attendance.SectionId)
                        .Select(s => s.Name)
                        .FirstOrDefaultAsync() ?? "";

                worksheet.Cell(row, 4).Value =
                    await _context.Subjects
                        .Where(s => s.Id == attendance.SubjectId)
                        .Select(s => s.Name)
                        .FirstOrDefaultAsync() ?? "";

                worksheet.Cell(row, 5).Value = attendance.TeacherId;
                worksheet.Cell(row, 6).Value = attendance.Date;
                worksheet.Cell(row, 7).Value = attendance.Status;

                if (attendance.CheckInTime.HasValue)
                {
                    worksheet.Cell(row, 8).Value =
                        attendance.CheckInTime.Value;
                }

                row++;
            }

            // Header ko bold aur blue karo
            var header = worksheet.Range("A1:H1");

            header.Style.Font.Bold = true;
            header.Style.Alignment.Horizontal =
                XLAlignmentHorizontalValues.Center;

            header.Style.Alignment.Vertical =
                XLAlignmentVerticalValues.Center;

            // Saare data ko center karo
            worksheet.RangeUsed().Style.Alignment.Horizontal =
                XLAlignmentHorizontalValues.Center;

            worksheet.RangeUsed().Style.Alignment.Vertical =
                XLAlignmentVerticalValues.Center;

            // Column width automatically set
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }



        public async Task ImportFromExcelAsync(IFormFile file, int teacherId)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Excel file is required.");

            using var stream = file.OpenReadStream();
            using var workbook = new XLWorkbook(stream);

            var worksheet = workbook.Worksheet(1);

            var usedRange = worksheet.RangeUsed();

            if (usedRange == null)
                throw new Exception("Excel file is empty.");

            var rows = usedRange.RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                var studentName = row.Cell(1).GetString().Trim();
                var className = row.Cell(2).GetString().Trim();
                var sectionName = row.Cell(3).GetString().Trim();
                var subjectName = row.Cell(4).GetString().Trim();

                if (string.IsNullOrWhiteSpace(studentName))
                    continue;

                // Column 5 = Date
                DateTime date;

                try
                {
                    date = row.Cell(5).GetValue<DateTime>();
                }
                catch
                {
                    throw new Exception(
                        $"Invalid Date at Excel row {row.RowNumber()}.");
                }

                // Column 6 = Status
                var status = row.Cell(6).GetString().Trim();

                var allowedStatuses = new[]
                {
            "Present", 
            "Absent",
            "Late",
            "Leave"
        };

                if (!allowedStatuses.Contains(status))
                {
                    throw new Exception(
                        $"Invalid status at Excel row {row.RowNumber()}: {status}");
                }

                // Column 7 = Check In Time
                DateTime? checkInTime = null;

                var timeText = row.Cell(7)
                    .GetFormattedString()
                    .Trim();

                if (!string.IsNullOrWhiteSpace(timeText))
                {
                    if (DateTime.TryParse(timeText, out var parsedDateTime))
                    {
                        checkInTime = date.Date.Add(
                            parsedDateTime.TimeOfDay);
                    }
                    else
                    {
                        throw new Exception(
                            $"Invalid Check In Time at Excel row {row.RowNumber()}: {timeText}");
                    }
                }

                // Student
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Name == studentName);

                // Class
                var classData = await _context.Classes
                    .FirstOrDefaultAsync(c => c.Name == className);

                // Section
                var section = await _context.Sections
                    .FirstOrDefaultAsync(s => s.Name == sectionName);

                // Subject
                var subject = await _context.Subjects
                    .FirstOrDefaultAsync(s => s.Name == subjectName);

                if (student == null)
                    throw new Exception(
                        $"Student not found: {studentName}");

                if (classData == null)
                    throw new Exception(
                        $"Class not found: {className}");

                if (section == null)
                    throw new Exception(
                        $"Section not found: {sectionName}");

                if (subject == null)
                    throw new Exception(
                        $"Subject not found: {subjectName}");

                // Duplicate check
                var alreadyExists = await _context.Attendances
                    .AnyAsync(a =>
                        a.StudentId == student.Id && 
                        a.ClassId == classData.Id &&
                        a.SectionId == section.Id &&
                        a.SubjectId == subject.Id &&
                        a.TeacherId == teacherId &&
                        a.Date.Date == date.Date);

                if (alreadyExists)
                    continue;

                // Create attendance
                var attendance = new Attendance
                {
                    StudentId = student.Id,
                    ClassId = classData.Id,
                    SectionId = section.Id,
                    SubjectId = subject.Id,

                    // TeacherId JWT se aa raha hai
                    TeacherId = teacherId,

                    Date = date,
                    Status = status,
                    CheckInTime = checkInTime,
                    CreatedAt = DateTime.Now
                };

                _context.Attendances.Add(attendance);
            }

            await _context.SaveChangesAsync();
        }



    }
}