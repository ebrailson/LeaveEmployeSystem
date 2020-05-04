using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeaveEmployeSystem.Models.ViewModel
{
    public class LeaveAllocationViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Number Of Days")]
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public int Period { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public string EmployeeId { get; set; }
        public LeaveTypeViewModel LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

    }

    public class CreateLeaveAllocationTypeViewModel
    {
        public int NumberUpdated { get; set; }
        public List<LeaveTypeViewModel> LeaveType { get; set; }
    }

    public class EditLeaveAllocationTypeViewModel
    {
        public int Id { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public string EmployeeId { get; set; }
        [Required]
        [Range(1, 25, ErrorMessage = "Please enter valid number")]
        [Display(Name = "Number Of Days")]
        public int NumberOfDays { get; set; }
        public LeaveTypeViewModel LeaveType { get; set; }
    }

    public class ViewAllocationViewModel
    {
        public EmployeeViewModel Employee { get; set; }
        // public string EmployeeId { get; set; }
        public List<LeaveAllocationViewModel> LeaveAllocations { get; set; }

    }

}
