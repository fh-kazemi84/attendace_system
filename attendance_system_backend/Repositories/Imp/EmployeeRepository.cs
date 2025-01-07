
using attendance_system_backend.Data;
using attendance_system_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace attendance_system_backend.Repositories.Imp
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get all employees
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                return await _context.Employees
                    .Include(e => e.Address)
                    .Include(e => e.UserInfo)
                    .Include(e => e.AttendanceRecords)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not fetch all employees: {ex.Message}");
            }
        }

        //Get employee by id
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var existingEmployee = await _context.Employees
                    .Include(e => e.Address)
                    .Include(e => e.UserInfo)
                    .Include(e => e.AttendanceRecords)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if(existingEmployee != null)
                {
                    return existingEmployee;
                }
                else
                {
                    throw new Exception($"Could not find employee with ID: {id}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not fetch employee: {ex.Message}");
            }
        }

        //Add new employee at data source
        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            try
            {
                if(employee.Address != null)
                {
                    _context.Addresses.Add(employee.Address);
                }

                if (employee.UserInfo != null)
                {
                    _context.UserInfos.Add(employee.UserInfo);
                }

                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not add new employee. {ex.Message}");
            }
        }

        //Update an existing employee
        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                var existingEmployee = await _context.Employees
                    .Include(e => e.Address)
                    .Include(e => e.UserInfo)
                    .Include(e => e.AttendanceRecords)
                    .FirstOrDefaultAsync(e => e.Id == employee.Id);

                if (existingEmployee == null)
                {
                    throw new Exception($"Could not find employee with id: {employee.Id}");
                }

                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Email = employee.Email;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.HireDate = employee.HireDate;
                existingEmployee.Position = employee.Position;
                existingEmployee.Salary = employee.Salary;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.DepartmentId = employee.DepartmentId;

                existingEmployee.Address.Street = employee.Address.Street;
                existingEmployee.Address.City = employee.Address.City;
                existingEmployee.Address.State = employee.Address.State;
                existingEmployee.Address.PostalCode = employee.Address.PostalCode;
                existingEmployee.Address.Country = employee.Address.Country;

                existingEmployee.UserInfo.Username = employee.UserInfo.Username;
                existingEmployee.UserInfo.PasswordHash = employee.UserInfo.PasswordHash;
                existingEmployee.UserInfo.Role = employee.UserInfo.Role;

                foreach (var existingRecord in existingEmployee.AttendanceRecords)
                {
                    foreach( var record in employee.AttendanceRecords)
                    {
                        if(existingRecord.Id == record.Id)
                        {
                            existingRecord.Date = record.Date;
                            existingRecord.CheckInTime = record.CheckInTime;
                            existingRecord.CheckOutTime = record.CheckOutTime;
                            existingRecord.Status = record.Status;
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return existingEmployee;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update employee {ex.Message}");
            }
        }

        //Delete an employee by id
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                var existingEmployee = await _context.Employees
                    .Include(e => e.Address)
                    .Include(e => e.UserInfo)
                    .Include(e => e.AttendanceRecords)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if(existingEmployee == null)
                {
                    return false;
                }

                var exsistingAddress = await _context.Addresses.FindAsync(existingEmployee.Address.Id);
                if (exsistingAddress != null)
                {
                    _context.Addresses.Remove(exsistingAddress);
                }

                var exsistingUserInfo = await _context.UserInfos.FindAsync(existingEmployee.UserInfo.Id);
                if (exsistingAddress != null)
                {
                    _context.UserInfos.Remove(exsistingUserInfo);
                }

                foreach (var record in existingEmployee.AttendanceRecords)
                {
                    _context.AttendanceRecords.Remove(record);
                }

                _context.Employees.Remove(existingEmployee);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not delete employee with ID {id}: {ex.Message}");
            }
        }

        //Get attendance-records by employeeId
        public async Task<IEnumerable<AttendanceRecord>> GetAttendanceRecordsByEmployeeIdAsync(int employeeId)
        {
            try
            {
                var employee = await _context.Employees
                    .Include(e => e.AttendanceRecords)
                    .FirstOrDefaultAsync(e => e.Id == employeeId);
                return employee?.AttendanceRecords ?? new List<AttendanceRecord>();
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not fetch attendance-records: {ex.Message}");
            }
        }

        //Get attendance-record by employeeId and attendancerecordId
        public async Task<AttendanceRecord> GetAttendanceRecordByEmployeeIdAndAttendancerecordIdAsync(int employeeId, int attendanceRecordId)
        {
            try
            {
                var employee = await GetEmployeeByIdAsync(employeeId);
                if (employee == null)
                {
                    throw new Exception($"Could not find employee with id: {employeeId}");
                }

                var existingAttendanceRecord = employee.AttendanceRecords
                    .FirstOrDefault(ar => ar.Id == attendanceRecordId);
                if (existingAttendanceRecord == null)
                {
                    throw new Exception($"Could not find attendance-record with id: {attendanceRecordId}");
                }

                return existingAttendanceRecord;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update employee {ex.Message}");
            }
        }

        //Add attendance-recod to data source
        public async Task<AttendanceRecord> AddAttendanceRecodAsync(int employeeId, AttendanceRecord attendanceRecord)
        {
            try
            {
                var employee = await GetEmployeeByIdAsync(employeeId);
                if(employee != null)
                {
                    employee.AttendanceRecords.Add(attendanceRecord);
                    await _context.SaveChangesAsync();
                    return attendanceRecord;
                }
                else
                {
                    throw new Exception($"Could not find employee with ID: {employeeId}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not add new attendance-record in this employee: {ex.Message}");
            }        
        }

        //Update existing attendance-record
        public async Task<AttendanceRecord> UpdateAttendanceRecodAsync(int employeeId, AttendanceRecord attendanceRecord)
        {
            try
            {
                var employee = await GetEmployeeByIdAsync(employeeId);
                if (employee == null)
                {
                    throw new Exception($"Could not find employee with id: {employeeId}");
                }

                var existingAttendanceRecord = employee.AttendanceRecords
                    .FirstOrDefault(ar => ar.Id == attendanceRecord.Id);
                if (existingAttendanceRecord == null)
                {
                    throw new Exception($"Could not find attendance-record with id: {attendanceRecord.Id}");
                }

                existingAttendanceRecord.Date = attendanceRecord.Date;
                existingAttendanceRecord.CheckInTime = attendanceRecord.CheckInTime;
                existingAttendanceRecord.CheckOutTime = attendanceRecord.CheckOutTime;
                existingAttendanceRecord.Status = attendanceRecord.Status;

                await _context.SaveChangesAsync();
                return existingAttendanceRecord;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update employee {ex.Message}");
            }
        }

        //Delete an attendance-recrod by attendanceRecordId and employeeId
        public async Task<bool> DeleteAttendanceRecodAsync(int attendanceRecordId, int employeeId)
        {
            try
            {
                var employee = await GetEmployeeByIdAsync(employeeId);
                if (employee == null)
                {
                    throw new Exception($"Could not find employee with ID: {employeeId}");
                }
                var attendanceRecord = employee.AttendanceRecords
                    .FirstOrDefault(ar => ar.Id == attendanceRecordId);
                if (attendanceRecord == null)
                {
                    throw new Exception($"Could not find attendance-record with id: {attendanceRecordId}");
                }

                _context.AttendanceRecords.Remove(attendanceRecord);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not delete attendance-record with ID {attendanceRecordId}: {ex.Message}");
            }
        }
    }
}