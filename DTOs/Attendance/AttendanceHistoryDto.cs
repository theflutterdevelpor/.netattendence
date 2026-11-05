namespace AttendanceSystem.DTOs.Attendance
{
    public class AttendanceHistoryDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public string ClassName { get; set; } = string.Empty;

        public string SectionName { get; set; } = string.Empty;

        public string SubjectName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime? CheckInTime { get; set; }
    }
}