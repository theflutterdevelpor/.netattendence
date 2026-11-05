namespace AttendanceSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FatherName { get; set; }

        public string RollNumber { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int ClassId { get; set; }

        public int SectionId { get; set; }

        public DateTime CreatedAt { get; set; }

        public Class? Class { get; set; }
        public Section? Section { get; set; }
    }
}
