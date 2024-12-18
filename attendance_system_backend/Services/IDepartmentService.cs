using attendance_system_backend.DTOs;

namespace attendance_system_backend.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync();
        Task<DepartmentDTO> GetDepartmentByIdAsync(int id);
        Task<DepartmentDTO> AddDepartmentAsync(DepartmentDTO departmentDto);
        Task<DepartmentDTO> UpdateDepartmentAsync(DepartmentDTO departmentDto);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
