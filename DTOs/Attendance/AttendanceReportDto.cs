namespace AttendanceSystem.DTOs.Attendance
{
    public class AttendanceReportDto
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public int TotalDays { get; set; }

        public int Present { get; set; }

        public int Absent { get; set; }

        public int Late { get; set; }

        public int Leave { get; set; }

        public double AttendancePercentage { get; set; }
    }
}