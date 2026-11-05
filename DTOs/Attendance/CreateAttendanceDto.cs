using AttendanceSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.DTOs.Attendance
{
    public class CreateAttendanceDto
    {
        [Required(ErrorMessage = "Student is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid student")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Class is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid class")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Section is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid section")]
        public int SectionId { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid subject")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("^(Present|Absent|Late|Leave)$",
      ErrorMessage = "Status must be Present, Absent, Late or Leave")]
        public string Status { get; set; } = string.Empty;


        public DateTime? CheckInTime { get; set; }
    }
}
