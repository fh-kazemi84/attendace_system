using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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