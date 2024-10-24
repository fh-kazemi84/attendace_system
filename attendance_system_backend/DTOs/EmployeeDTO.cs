namespace attendance_system_backend.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        public int Gender { get; set; } // Assuming Gender enum is an int
        public AddressDTO AddressDTO { get; set; }
        public int DepartmentId { get; set; }
        public UserInfoDTO UserInfoDTO { get; set; }
        public ICollection<AttendanceRecordDTO> AttendanceRecordDTOs { get; set; }

        public EmployeeDTO()
        {
            AttendanceRecordDTOs = new List<AttendanceRecordDTO>();
        }
    }
}