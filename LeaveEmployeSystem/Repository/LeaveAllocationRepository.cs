using LeaveEmployeSystem.Data;
using LeaveEmployeSystem.Data.Entities;
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
            return _context.LeaveAllocations.ToList();
        }

        public LeaveAllocation FindById(int id)
        {
            var leaveAllocation = _context.LeaveAllocations.FirstOrDefault(l => l.Id == id);
            return leaveAllocation;
        }

        public bool isExists(int id)
        {
            var exists = _context.LeaveTypes.Any(l => l.Id == id);
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
