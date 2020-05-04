using AutoMapper;
using LeaveEmployeSystem.Data.Entities;
using LeaveEmployeSystem.Models.ViewModel;

namespace LeaveEmployeSystem.Mapping
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<LeaveType, LeaveTypeViewModel>().ReverseMap();


            CreateMap<LeaveHistory, LeaveHistoryViewModel>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationViewModel>().ReverseMap();

            CreateMap<LeaveAllocation, EditLeaveAllocationTypeViewModel>().ReverseMap();


            CreateMap<Employee, EmployeeViewModel>().ReverseMap();

        }
    }
}
