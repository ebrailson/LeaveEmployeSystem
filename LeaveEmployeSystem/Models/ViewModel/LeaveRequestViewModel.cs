using LeaveEmployeSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeaveEmployeSystem.Models.ViewModel
{
    public class LeaveRequestViewModel
    {

        public int Id { get; set; }
        public Employee RequestingEmployee { get; set; }
        [DisplayName("Employee Name")]
        public string RequestingEmployeeId { get; set; }
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        public LeaveType LeaveType { get; set; }
        [DisplayName("Leave Type")]
        public int LeaveTypeId { get; set; }
        [DisplayName("Date Requested")]
        public DateTime DateRequested { get; set; }
        [DisplayName("Date Actioned")]
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
        public Employee ApprovedBy { get; set; }
        [DisplayName("Employee Name")]
        public string ApprovedById { get; set; }
        [MaxLength(200)]
        [DisplayName("Request Comments")]
        public string RequestsComment { get; set; }


    }

    public class AdminViewRequestViewModel
    {
        [Display(Name = "Total Numbers Of Requests")]
        public int TotalRequests { get; set; }
        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }
        [Display(Name = "Pendings Requests")]
        public int PendingRequests { get; set; }
        [Display(Name = "Rejected Requests")]
        public int RejectedRequests { get; set; }
        public List<LeaveRequestViewModel> LeaveRequests { get; set; }

    }



    public class CreateLeaveRequestViewModel
    {
        [DisplayName("Start Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [MaxLength(200)]
        [DisplayName("Request Comments")]
        public string RequestsComment { get; set; }


        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }


    }
    public class EmployeeRequestViewModel
    {
        public List<LeaveRequestViewModel> LeaveRequests { get; set; }
        public List<LeaveAllocationViewModel> LeaveAllocations { get; set; }
    }
}
