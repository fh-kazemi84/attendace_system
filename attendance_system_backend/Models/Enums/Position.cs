namespace attendance_system_backend.Models.Enums
{
    public enum Position
    {
        Manager,
        Senior,
        Junior,
        Office_worker
    }

    public static class PositionExtensions
    {
        public static string ToFriendlyString(this Position position)
        {
            return position switch
            {
                Position.Manager => "Manager",
                Position.Senior => "Senior",
                Position.Junior => "Junior",
                Position.Office_worker => "Office Worker",
                _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
            } ;
        }
    }
}
