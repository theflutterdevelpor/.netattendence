namespace AttendanceSystem.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ClassId { get; set; }

        public int SectionId { get; set; }

        public int SubjectId { get; set; }

        public int TeacherId { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime? CheckInTime { get; set; }

        public DateTime CreatedAt { get; set; }



        public Student? Student { get; set; }
        public Class? Class { get; set; }
        public Section? Section { get; set; }
        public Subject? Subject { get; set; }
    }
}
