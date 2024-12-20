using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using attendance_system_backend.Models.Enums;

namespace attendance_system_backend.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(15, ErrorMessage = "Phone number can't be longer than 15 characters.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Hire date is required.")]
        [DataType(DataType.Date)] //date is stored without time
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        public Position Position { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value.")]
        public double Salary { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "AddressId is required.")]
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        [Required(ErrorMessage = "DepartmentId is required.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "UserInfoId is required.")]
        public int UserInfoId { get; set; }

        [ForeignKey("UserInfoId")]
        public UserInfo UserInfo { get; set; }

        public ICollection<AttendanceRecord> AttendanceRecords { get; set; }

        public Employee()
        {
            AttendanceRecords = new List<AttendanceRecord>();
        }
    }
}