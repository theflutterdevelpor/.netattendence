using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.DTOs.LeaveRequest
{
    public class CreateLeaveRequestDto
    {
        [Required(ErrorMessage = "Student is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid student")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "From date is required")]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage ="To date is required")]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Reason must be 5-500 characters")]
        public string Reason { get; set; } = string.Empty;
    }
}