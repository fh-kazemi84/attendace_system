
using attendance_system_backend.DTOs;

namespace attendance_system_backend.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeByIdAsync(int id);
        Task<EmployeeDTO> AddEmployeeAsync(EmployeeDTO employeeDto);
        Task<EmployeeDTO> UpdateEmployeeAsync(EmployeeDTO employeeDto);
        Task<bool> DeleteEmployeeAsync(int id);

    }
}