using attendance_system_backend.DTOs;
using attendance_system_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace attendance_system_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //GET: api/Employee/employees
        [HttpGet("employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                var employees = await _employeeService.GetAllEmployeesAsync();
                return Ok(employees);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //GET: api/Employee/employees/id
        [HttpGet("employees/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
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

        //POST: api/Employee/add
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

                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                var createdEmployee = await _employeeService.AddEmployeeAsync(employeeDto);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id}, createdEmployee);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //PUT: api/Employee/update?id=
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

        //DELETE: api/Employee/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
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

        //GET: api/Employee/{employeeId}/attendances
        [HttpGet("{employeeId}/attendances")]
        public async Task<IActionResult> GetAttendanceRecords(int employeeId)
        {
            try
            {
                var attendanceRecords = await _employeeService.GetAttendanceRecordsByEmployeeIdAsync(employeeId);
                return Ok(attendanceRecords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //POST: api/Employee/{employeeId}/add-attendance
        [HttpPost("{employeeId}/add-attendance")]
        public async Task<IActionResult> AddAttendanceRecod(int employeeId, [FromBody] AttendanceRecordDTO attendanceRecordDto)
        {
            try
            {
                if (attendanceRecordDto == null)
                {
                    return BadRequest("Attendance-Record object is null.");
                }

                var createdAttendanceRecord = await _employeeService.AddAttendanceRecodAsync(employeeId, attendanceRecordDto);
                return CreatedAtAction(nameof(GetAttendanceRecords), new { id = createdAttendanceRecord.Id }, createdAttendanceRecord);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //PUT: api/Employee/{employeeId}/update-attendance?attendanceRecordId=
        [HttpPut("{employeeId}/update-attendance/{attendanceRecordId}")]
        public async Task<IActionResult> UpdateAttendanceRecord(int employeeId, int attendanceRecordId, [FromBody] AttendanceRecordDTO attendanceRecordDto)
        {
            try
            {
                if (attendanceRecordDto == null)
                {
                    return BadRequest("Attendance-Record data is null.");
                }

                attendanceRecordDto.Id = attendanceRecordId;
                var updatedAttendanceRecord = await _employeeService.UpdateAttendanceRecodAsync(employeeId, attendanceRecordDto);
                return Ok(updatedAttendanceRecord);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //DELETE: api/Employee/{employeeId}/delete-attendance/{attendanceRecordId}
        [HttpDelete("{employeeId}/delete-attendance/{attendanceRecordId}")]
        public async Task<IActionResult> DeleteAttendanceRecord(int attendanceRecordId, int employeeId)
        {
            try
            {
                var deletedAttendanceRecord = await _employeeService.DeleteAttendanceRecodAsync(attendanceRecordId, employeeId);
                if (!deletedAttendanceRecord)
                {
                    return NotFound();
                }
                return Ok("AttendanceRecord deleted successfully. ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
