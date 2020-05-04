using LeaveEmployeSystem.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LeaveEmployeSystem.Models.ViewModel;

namespace LeaveEmployeSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveHistory> LeaveHistories { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveEmployeSystem.Models.ViewModel.LeaveTypeViewModel> LeaveTypeViewModel { get; set; }
        public DbSet<LeaveEmployeSystem.Models.ViewModel.EmployeeViewModel> EmployeeViewModel { get; set; }
        public DbSet<LeaveEmployeSystem.Models.ViewModel.LeaveAllocationViewModel> LeaveAllocationViewModel { get; set; }
        public DbSet<LeaveEmployeSystem.Models.ViewModel.EditLeaveAllocationTypeViewModel> EditLeaveAllocationTypeViewModel { get; set; }
        //   public DbSet<LeaveEmployeSystem.Models.ViewModel.LeaveTypeViewModel> DetailsTypeViewModel { get; set; }
    }
}
