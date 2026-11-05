using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.DTOs.Student
{
    public class CreateStudentDto
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be 3-100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Father name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Father name must be 3-100 characters")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Roll number is required")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Roll number must be 1-20 characters")]
        public string RollNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(150)]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Class is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid class")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Section is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid section")]
        public int SectionId { get; set; }
    }
}
