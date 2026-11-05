namespace AttendanceSystem.Models
{
    public class Section
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ClassId { get; set; }

        public Class? Class { get; set; }
    }
}