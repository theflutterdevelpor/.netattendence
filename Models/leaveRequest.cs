namespace AttendanceSystem.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Reason { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; }

        public Student? Student { get; set; }
    }
}