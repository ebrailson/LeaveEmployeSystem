using LeaveEmployeSystem.Data;
using LeaveEmployeSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LeaveEmployeSystem.Repository
{
    public class LeaveRequestedRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveRequestedRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(LeaveRequested model)
        {
            _context.LeaveRequesteds.Add(model);
            return Save();
        }

        public bool Delete(LeaveRequested entity)
        {
            _context.LeaveRequesteds.Remove(entity);
            return Save();
        }

        public ICollection<LeaveRequested> FindAll()
        {
            return _context.LeaveRequesteds
                .Include(l => l.RequestingEmployee)
                .Include(l => l.ApprovedBy)
                .Include(l => l.LeaveType)
                .ToList();
        }

        public LeaveRequested FindById(int id)
        {
            var leaveHistory = _context.LeaveRequesteds
                 .Include(l => l.RequestingEmployee)
                .Include(l => l.ApprovedBy)
                .Include(l => l.LeaveType)
                .FirstOrDefault(l => l.Id == id);
            return leaveHistory;
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

        public bool Update(LeaveRequested model)
        {
            _context.LeaveRequesteds.Update(model);
            return Save();
        }
    }
}
