
using attendance_system_backend.DTOs;
using attendance_system_backend.Models;
using attendance_system_backend.Repositories;
using AutoMapper;

namespace attendance_system_backend.Services.Imp
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        //Get all employees
        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployeesAsync();
                return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
            }
            catch(Exception ex)
            {
                throw new Exception($"Error retreiving employees: {ex.Message}");
            }
        }

        //Get employee by id
        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                return _mapper.Map<EmployeeDTO>(employee);
            }
            catch(Exception ex)
            {
                throw new Exception($"Error retreiving employee: {ex.Message}");
            }
        }

        //Add new employee at data source
        public async Task<EmployeeDTO> AddEmployeeAsync(EmployeeDTO employeeDto)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                var addedEmployee = await _employeeRepository.AddEmployeeAsync(employee);
                return _mapper.Map<EmployeeDTO>(addedEmployee);
            }
            catch(Exception ex)
            {
                throw new Exception($"Error adding new employee: {ex.Message}");
            }
        }

        //Update an existing employee
        public async Task<EmployeeDTO> UpdateEmployeeAsync(EmployeeDTO employeeDto)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employee);
                return _mapper.Map<EmployeeDTO>(updatedEmployee);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating employee: {ex.Message}");
            }
        }

        //Delete an employee by id
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                return await _employeeRepository.DeleteEmployeeAsync(id);
            }
            catch(Exception ex)
            {
                throw new Exception($"Error deleting employee: {ex.Message}");
            }
        }

        //Get attendance-records by employeeId
        public async Task<IEnumerable<AttendanceRecordDTO>> GetAttendanceRecordsByEmployeeIdAsync(int employeeId)
        {
            try
            {
                var attendanceRecords = await _employeeRepository.GetAttendanceRecordsByEmployeeIdAsync(employeeId);
                return _mapper.Map<IEnumerable<AttendanceRecordDTO>>(attendanceRecords);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retreiving attendance-records: {ex.Message}");
            }
        }

        //Add attendance-recod to data source
        public async Task<AttendanceRecordDTO> AddAttendanceRecodAsync(int employeeId, AttendanceRecordDTO attendanceRecordDto)
        {
            try
            {
                var attendanceRecord = _mapper.Map<AttendanceRecord>(attendanceRecordDto);
                var addedAttendanceRecord = await _employeeRepository.AddAttendanceRecodAsync(employeeId, attendanceRecord);
                return _mapper.Map<AttendanceRecordDTO>(addedAttendanceRecord);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding new employee: {ex.Message}");
            }
        }

        //Update existing attendance-record
        public async Task<AttendanceRecordDTO> UpdateAttendanceRecodAsync(int employeeId, AttendanceRecordDTO attendanceRecordDto)
        {

            try
            {
                var attendanceRecord = _mapper.Map<AttendanceRecord>(attendanceRecordDto);
                var attendanceRecordUpdated = await _employeeRepository.UpdateAttendanceRecodAsync(employeeId, attendanceRecord);
                return _mapper.Map<AttendanceRecordDTO>(attendanceRecordUpdated);
            }
            catch(Exception ex)
            {
                throw new Exception($"Error updating attendance-record: {ex.Message}");
            }
        }

        //Delete an attendance-record by attendanceRecordId and employeeId
        public async Task<bool> DeleteAttendanceRecodAsync(int attendanceRecordId, int employeeId)
        {
            try
            {
                return await _employeeRepository.DeleteAttendanceRecodAsync(attendanceRecordId, employeeId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting attendance-record: {ex.Message}");
            }
        }
    }
}
