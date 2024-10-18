
using attendance_system_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace attendance_system_backend.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets for each model
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
    }
}