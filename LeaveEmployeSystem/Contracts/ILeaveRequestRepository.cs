using LeaveEmployeSystem.Contracts;
using LeaveEmployeSystem.Data.Entities;
using System.Collections.Generic;

namespace LeaveEmployeSystem.Repository
{
    public interface ILeaveRequestRepository : IRepositoryBase<LeaveRequested>
    {
        ICollection<LeaveRequested> GetLeaveRequestedByEmployee(string employee);
    }
}
