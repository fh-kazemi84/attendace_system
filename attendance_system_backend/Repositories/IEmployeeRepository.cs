using attendance_system_backend.Models;

namespace attendance_system_backend.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);

        Task<IEnumerable<AttendanceRecord>> GetAttendanceRecordsByEmployeeIdAsync(int employeeId);
        Task<AttendanceRecord> GetAttendanceRecordByEmployeeIdAndAttendancerecordIdAsync(int employeeId, int attendanceRecordId);
        Task<AttendanceRecord> AddAttendanceRecodAsync(int employeeId, AttendanceRecord attendanceRecord);
        Task<AttendanceRecord> UpdateAttendanceRecodAsync(int employeeId, AttendanceRecord attendanceRecord);
        Task<bool> DeleteAttendanceRecodAsync(int attendanceRecordId, int employeeId);
    }
}