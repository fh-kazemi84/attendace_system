namespace attendance_system_backend.Models.Enums
{
    public enum Gender
    {
        Male,
        Female
    }

    public static class GenderExtensions
    {
        public static string ToFriendlyString(this Gender gender)
        {
            return gender switch
            {
                Gender.Male => "Male",
                Gender.Female => "Female",
                _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
            };
        }
    }
}