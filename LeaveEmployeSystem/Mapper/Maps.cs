using AutoMapper;
using LeaveEmployeSystem.Data.Entities;
using LeaveEmployeSystem.Models.ViewModel;
using System;

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
        public class StringToDateTimeConverter : ITypeConverter<string, DateTime?>
        {
            public DateTime? Convert(string source, DateTime? destination, ResolutionContext context)
            {
                object objDateTime = source;
                return objDateTime == null ? default(DateTime) : DateTime.ParseExact(objDateTime.ToString(), @"d-M-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        public class DateTimeStringToConverter : ITypeConverter<DateTime?, string>
        {
            public string Convert(DateTime? source, string destination, ResolutionContext context)
            {
                return source?.ToString("d-M-yyyy") ?? "";

            }
        }

        public class DateTimeConverter : ITypeConverter<string, DateTime>
        {
            public DateTime Convert(string source, DateTime destination, ResolutionContext context)
            {
                object objDateTime = source;
                return objDateTime == null ? default(DateTime) : DateTime.ParseExact(objDateTime.ToString(), @"d-M-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        public class InvertDateTimeConverter : ITypeConverter<DateTime, string>
        {
            public string Convert(DateTime source, string destination, ResolutionContext context)
            {
                return source.ToString("d-M-yyyy") ?? "";
            }
        }

    }
}
