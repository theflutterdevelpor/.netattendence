using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.DTOs.Section
{
    public class UpdateSectionDto
    {
        [Required(ErrorMessage = "Section name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Section name must be 1-50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Class is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid class")]
        public int ClassId { get; set; }
    }
}
