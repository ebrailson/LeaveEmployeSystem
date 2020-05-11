using LeaveEmployeSystem.Data;
using LeaveEmployeSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaveEmployeSystem.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveAllocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CheckExistsAllocation(int leaveTypeId, string employee)
        {
            var period = DateTime.Now.Year;
            return FindAll()
                  .Where(l => l.LeaveTypeId == leaveTypeId && l.EmployeeId == employee && l.Period == period)
                  .Any();
        }

        public bool Create(LeaveAllocation model)
        {
            _context.LeaveAllocations.Add(model);
            return Save();
        }

        public bool Delete(LeaveAllocation model)
        {
            _context.LeaveAllocations.Remove(model);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            return _context.LeaveAllocations
                .Include(l => l.LeaveType)
                .Include(l => l.Employee)
                .ToList();
        }

        public LeaveAllocation FindById(int id)
        {
            var leaveAllocation = _context.LeaveAllocations
                .Include(l => l.LeaveType)
                .Include(l => l.Employee)
                .FirstOrDefault(l => l.Id == id);
            return leaveAllocation;
        }



        public ICollection<LeaveAllocation> GetLeaveAllocationById(string employee)
        {
            var period = DateTime.Now.Year;
            return FindAll()
                 .Where(l => l.EmployeeId == employee && l.Period == period).ToList();
        }

        public LeaveAllocation GetLeaveAllocationByEmployeeAndType(string employee, int leaveType)
        {
            var period = DateTime.Now.Year;
            return FindAll()
                  .FirstOrDefault(l => l.EmployeeId == employee && l.Period == period && l.LeaveTypeId == leaveType);
        }
        public bool isExists(int id)
        {
            var exists = _context.LeaveAllocations.Any(l => l.Id == id);
            return exists;
        }

        public bool Save()
        {
            var rows = _context.SaveChanges();
            return rows > 0;
        }

        public bool Update(LeaveAllocation model)
        {
            _context.LeaveAllocations.Update(model);
            return Save();
        }
    }
}
