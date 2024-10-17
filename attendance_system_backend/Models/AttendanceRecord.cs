
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

        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public AttendanceRecord(){ }
        public AttendanceRecord(Employee employee)
        {
            Date = DateTime.UtcNow; // Initialize date to the current date and time in UTC
            Status = AttendanceStatus.Absent; // Set default status to Absent
            //Employee = employee ?? throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
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