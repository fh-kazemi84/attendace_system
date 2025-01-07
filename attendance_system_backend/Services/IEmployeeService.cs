using attendance_system_backend.DTOs;
using attendance_system_backend.Models;

namespace attendance_system_backend.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeByIdAsync(int id);
        Task<EmployeeDTO> AddEmployeeAsync(EmployeeDTO employeeDto);
        Task<EmployeeDTO> UpdateEmployeeAsync(EmployeeDTO employeeDto);
        Task<bool> DeleteEmployeeAsync(int id);

        Task<IEnumerable<AttendanceRecordDTO>> GetAttendanceRecordsByEmployeeIdAsync(int employeeId);
        Task<AttendanceRecordDTO> GetAttendanceRecordByEmployeeIdAndAttendancerecordIdAsync(int employeeId, int attendanceRecordId);
        Task<AttendanceRecordDTO> AddAttendanceRecodAsync(int employeeId, AttendanceRecordDTO attendanceRecordDto);
        Task<AttendanceRecordDTO> UpdateAttendanceRecodAsync(int employeeId, AttendanceRecordDTO attendanceRecordDto);
        Task<bool> DeleteAttendanceRecodAsync(int attendanceRecordId, int employeeId);
    }
}
