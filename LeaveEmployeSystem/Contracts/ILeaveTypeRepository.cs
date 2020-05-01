using LeaveEmployeSystem.Contracts;
using LeaveEmployeSystem.Data.Entities;
using System.Collections.Generic;

namespace LeaveEmployeSystem.Repository
{
    public interface ILeaveTypeRepository : IRepositoryBase<LeaveType>
    {
        ICollection<LeaveType> GetEmployeeByLeaveType(int id);
    }
}
