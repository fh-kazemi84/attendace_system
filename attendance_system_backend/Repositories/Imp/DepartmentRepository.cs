using attendance_system_backend.Data;
using attendance_system_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace attendance_system_backend.Repositories.Imp
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get all departments
        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            try
            {
                return await _context.Departments.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not fetch all departments: {ex.Message}");
            }
        }

        //Get department by id
        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            try
            {
                var existingDepartment = await _context.Departments.FindAsync(id);

                if (existingDepartment != null)
                {
                    return existingDepartment;
                }
                else
                {
                    throw new Exception($"Could not find department with ID: {id}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not fetch department: {ex.Message}");
            }
        }

        //Add new department at data source
        public async Task<Department> AddDepartmentAsync(Department department)
        {
            try
            {
                await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();
                return department;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not add new department. {ex.Message}");
            }
        }

        //Update an existing department
        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            try
            {
                var existingDepartment = await _context.Departments.FindAsync(department.Id);

                if (existingDepartment == null)
                {
                    throw new Exception($"Could not find department with id: {department.Id}");
                }

                existingDepartment.Name = department.Name;
                existingDepartment.Description = department.Description;
                
                await _context.SaveChangesAsync();
                return existingDepartment;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update department {ex.Message}");
            }
        }

        //Delete a department by id
        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            try
            {
                var existingDepartment = await _context.Departments.FindAsync(id);

                if (existingDepartment == null)
                {
                    return false;
                }

                _context.Departments.Remove(existingDepartment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not delete department with ID {id}: {ex.Message}");
            }
        }
    }
}
