
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using attendance_system_backend.Models.Enums;

namespace attendance_system_backend.Models
{
    public class AttendanceRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Attendance date is required.")]
        public DateTime Date { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        [Required(ErrorMessage = "Attendance status is required.")]
        public AttendanceStatus Status { get; set; }

        public AttendanceRecord()
        {
            Date = DateTime.UtcNow;
            Status = AttendanceStatus.Absent;       
        }

        public double? GetTotalHoursWorked()
        {
            if (CheckInTime.HasValue && CheckOutTime.HasValue)
            {
                return (CheckOutTime.Value - CheckInTime.Value).TotalHours;
            }
            return null;
        }
    }
}