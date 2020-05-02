using LeaveEmployeSystem.Data;
using LeaveEmployeSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaveEmployeSystem.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(LeaveType model)
        {
            _context.LeaveTypes.Add(model);
            return Save();

        }

        public bool Delete(LeaveType model)
        {
            _context.LeaveTypes.Remove(model);
            return Save();

        }

        public ICollection<LeaveType> FindAll()
        {
            return _context.LeaveTypes.ToList();
        }

        public LeaveType FindById(int id)
        {
            var leaveType = _context.LeaveTypes.FirstOrDefault(l => l.Id == id);
            // or you can use _context.LeaveTypes.Find(id);
            return leaveType;
        }

        public ICollection<LeaveType> GetEmployeeByLeaveType(int id)
        {
            throw new NotImplementedException();
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
            //   if (rows > 1) return true; return false;

        }

        public bool Update(LeaveType model)
        {
            _context.LeaveTypes.Update(model);
            return Save();
        }
    }
}
