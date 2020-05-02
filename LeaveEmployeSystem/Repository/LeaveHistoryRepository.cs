using LeaveEmployeSystem.Data;
using LeaveEmployeSystem.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LeaveEmployeSystem.Repository
{
    public class LeaveHistoryRepository : ILeaveHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(LeaveHistory model)
        {
            _context.LeaveHistories.Add(model);
            return Save();
        }

        public bool Delete(LeaveHistory entity)
        {
            _context.LeaveHistories.Remove(entity);
            return Save();
        }

        public ICollection<LeaveHistory> FindAll()
        {
            return _context.LeaveHistories.ToList();
        }

        public LeaveHistory FindById(int id)
        {
            var leaveHistory = _context.LeaveHistories.FirstOrDefault(l => l.Id == id);
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

        public bool Update(LeaveHistory model)
        {
            _context.LeaveHistories.Update(model);
            return Save();
        }
    }
}
