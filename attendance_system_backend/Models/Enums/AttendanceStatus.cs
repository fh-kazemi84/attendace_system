namespace attendance_system_backend.Models.Enums
{
    public enum AttendanceStatus
    {
        
        Present,
        Absent,
        Leave,
        HalfDay,
        Late,
        EarlyLeave
    }

    public static class AttendanceStatusExtensions
    {
        public static string ToFriendlyString(this AttendanceStatus status)
        {
            return status switch
            {
                AttendanceStatus.Present => "Present",
                AttendanceStatus.Absent => "Absent",
                AttendanceStatus.Leave => "On Leave",
                AttendanceStatus.HalfDay => "Half Day",
                AttendanceStatus.Late => "Late",
                AttendanceStatus.EarlyLeave => "Early Leave",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}
