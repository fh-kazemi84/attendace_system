
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using attendance_system_backend.Models.Enums;

namespace attendance_system_backend.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "User role is required.")]
        public UserRole Role { get; set; }

    }
}