using attendance_system_backend.DTOs;
using attendance_system_backend.Models;
using attendance_system_backend.Repositories;
using AutoMapper;

namespace attendance_system_backend.Services.Imp
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        //Get all departments
        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync()
        {
            try
            {
                var departments = await _departmentRepository.GetAllDepartmentsAsync();
                return _mapper.Map<IEnumerable<DepartmentDTO>>(departments);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retreiving departments: {ex.Message}");
            }
        }

        //Get department by id
        public async Task<DepartmentDTO> GetDepartmentByIdAsync(int id)
        {
            try
            {
                var department = await _departmentRepository.GetDepartmentByIdAsync(id);
                return _mapper.Map<DepartmentDTO>(department);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retreiving department: {ex.Message}");
            }
        }

        //Add new department at data source
        public async Task<DepartmentDTO> AddDepartmentAsync(DepartmentDTO departmentDto)
        {
            try
            {
                var department = _mapper.Map<Department>(departmentDto);
                var addedDepartment = await _departmentRepository.AddDepartmentAsync(department);
                return _mapper.Map<DepartmentDTO>(addedDepartment);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding new department: {ex.Message}");
            }
        }

        //Update an existing department
        public async Task<DepartmentDTO> UpdateDepartmentAsync(DepartmentDTO departmentDto)
        {
            try
            {
                var department = _mapper.Map<Department>(departmentDto);
                var updatedDepartment = await _departmentRepository.UpdateDepartmentAsync(department);
                return _mapper.Map<DepartmentDTO>(updatedDepartment);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating department: {ex.Message}");
            }
        }

        //Delete a department by id
        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            try
            {
                return await _departmentRepository.DeleteDepartmentAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting department: {ex.Message}");
            }
        }        
    }
}
