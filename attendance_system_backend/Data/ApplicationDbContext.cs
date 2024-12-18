
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
        public DbSet<UserInfo> UserInfos { get; set; }  
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Address)
                .WithOne() //Assuming an employee can a address, and no reverse navigation
                .HasForeignKey<Employee>(e => e.AddressId);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.UserInfo)
                .WithOne()
                .HasForeignKey<Employee>(e => e.UserInfoId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AttendanceRecords)
                .WithOne();

            base.OnModelCreating(modelBuilder);
        }

    }
}