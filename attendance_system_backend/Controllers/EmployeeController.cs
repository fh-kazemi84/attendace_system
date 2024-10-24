
using attendance_system_backend.DTOs;
using attendance_system_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace attendance_system_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController  : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                return Ok(employees);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //GET: api/employee/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if(employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //POST: api/employee
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDTO employeeDto)
        {
            try
            {
                if(employeeDto == null)
                {
                    return BadRequest("Employee object is null.");
                }

                var createdEmployee = await _employeeService.AddEmployeeAsync(employeeDto);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id}, createdEmployee);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //PUT: api/employee
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDTO employeeDto)
        {
            try
            {
                if (employeeDto == null)
                {
                    return BadRequest("Employee data is null.");
                }

                employeeDto.Id = id;
                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(employeeDto);
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //DELETE: api/employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var deletedEmployee = await _employeeService.DeleteEmployeeAsync(id);
                if (!deletedEmployee)
                {
                    return NotFound();
                }
                return Ok("Employee deleted successfully. ");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
