using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.DTOs.Subject
{
    public class UpdateSubjectDto
    {
        [Required(ErrorMessage = "Subject name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Subject name must be 1-100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Class is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid class")]
        public int ClassId { get; set; }
    }
}
