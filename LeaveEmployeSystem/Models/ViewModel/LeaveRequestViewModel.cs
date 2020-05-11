using LeaveEmployeSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeaveEmployeSystem.Models.ViewModel
{
    public class LeaveRequestViewModel
    {

        public int Id { get; set; }
        public Employee RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }
        public Employee ApprovedBy { get; set; }
        public string ApprovedById { get; set; }


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
        [Display(Name = "Start Date")]
        [Required]
        public string StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required]
        public string EndDate { get; set; }

        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }



    }
}
