using AutoMapper;
using LeaveEmployeSystem.Data.Entities;
using LeaveEmployeSystem.Models.ViewModel;

namespace LeaveEmployeSystem.Mapping
{
    public class Maps : Profile
    {
        public Maps()
        {
            /*
                        CreateMap<DateTime, string>().ConvertUsing(d => d.ToString("dd/MM/yyyy"));

                        CreateMap<string, DateTime?>().ConvertUsing<StringToDateTimeConverter>();
                        CreateMap<string, DateTime>().ConvertUsing<DateTimeConverter>();
            */
            CreateMap<LeaveType, LeaveTypeViewModel>().ReverseMap();

            CreateMap<LeaveRequestViewModel, LeaveRequested>().ReverseMap();


            CreateMap<LeaveAllocation, LeaveAllocationViewModel>().ReverseMap();

            CreateMap<LeaveAllocation, EditLeaveAllocationTypeViewModel>().ReverseMap();


            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }

    }
}
