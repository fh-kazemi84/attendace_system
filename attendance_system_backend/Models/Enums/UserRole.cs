namespace attendance_system_backend.Models.Enums
{
    public enum UserRole
    { 
        Admin, 
        Employee
    }

     public static class UserRoleExtensions
    {
        public static string ToFriendlyString(this UserRole userRole)
        {
            return userRole switch
            {
                UserRole.Admin=> "Admin",
                UserRole.Employee => "Employee",
                _ => throw new ArgumentOutOfRangeException(nameof(userRole), userRole, null)
            };
        }
    }
}