using attendance_system_backend.Models.Enums;

namespace attendance_system_backend.DTOs
{
    public class UserInfoDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int Role { get; set; }
    }
}
