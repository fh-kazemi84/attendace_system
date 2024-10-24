using attendance_system_backend.DTOs;
using attendance_system_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace attendance_system_backend.Controllers
{
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        //GET: api/departments
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _departmentService.GetAllDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //GET: api/department/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentByIdAsync(id);
                if (department == null)
                {
                    return NotFound();
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //POST: api/department
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentDTO departmentDto)
        {
            try
            {
                if (departmentDto == null)
                {
                    return BadRequest("Department object is null.");
                }

                var createdDepartment = await _departmentService.AddDepartmentAsync(departmentDto);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.Id }, createdDepartment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //PUT: api/department
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDTO departmentDto)
        {
            try
            {
                if (departmentDto == null)
                {
                    return BadRequest("Employee data is null.");
                }

                departmentDto.Id = id;
                var updatedDepartment = await _departmentService.UpdateDepartmentAsync(departmentDto);
                return Ok(updatedDepartment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //DELETE: api/department/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var deletedDepartment = await _departmentService.DeleteDepartmentAsync(id);
                if (!deletedDepartment)
                {
                    return NotFound();
                }
                return Ok("Department deleted successfully. ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}