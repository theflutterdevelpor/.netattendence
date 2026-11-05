using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.DTOs.Class
{
    public class UpdateClassDto
    {
        [Required(ErrorMessage = "Class name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Class name must be 1-50 characters")]
        public string Name { get; set; }
    }
}
