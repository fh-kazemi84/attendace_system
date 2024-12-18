using attendance_system_backend.Models.Enums;

namespace attendance_system_backend.DTOs
{
    public class AttendanceRecordDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }
        public int Status { get; set; }     

        public AttendanceRecordDTO()
        {
            Date = DateTime.UtcNow;
            Status = (int) AttendanceStatus.Absent;
        }
    }
}
