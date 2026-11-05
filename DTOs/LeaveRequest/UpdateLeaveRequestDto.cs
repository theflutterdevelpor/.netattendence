using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.DTOs.LeaveRequest
{
    public class UpdateLeaveRequestDto
    {
        [Required(ErrorMessage = "From date is required")]

        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "To date is required")]

        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Reason must be 5-500 characters")]
        public string Reason { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("^(Pending|Approved|Rejected)$",
      ErrorMessage = "Status must be Pending, Approved or Rejected")]
        public string Status { get; set; } = string.Empty;
    }
}