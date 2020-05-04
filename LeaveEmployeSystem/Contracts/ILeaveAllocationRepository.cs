using LeaveEmployeSystem.Contracts;
using LeaveEmployeSystem.Data.Entities;
using System.Collections.Generic;

namespace LeaveEmployeSystem.Repository
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        bool CheckExistsAllocation(int leaveTypeId, string employee);
        ICollection<LeaveAllocation> GetLeaveAllocationById(string id);
    }
}
