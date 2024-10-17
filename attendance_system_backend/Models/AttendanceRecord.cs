
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public AttendanceRecord(Employee employee)
        {
            Date = DateTime.UtcNow; // Initialize date to the current date and time in UTC
            Employee = employee ?? throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
        }
    }
}