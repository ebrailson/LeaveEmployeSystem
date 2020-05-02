using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeaveEmployeSystem.Models.ViewModel
{
    public class LeaveAllocationViewModel
    {
        public int Id { get; set; }
        [Required]
        public int NumberOdDays { get; set; }
        public DateTime DateCreated { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public string EmployeeId { get; set; }
        public LeaveTypeViewModel LeaveType { get; set; }
        public string LeaveTypeId { get; set; }

        //Dropdown
        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }

    }
}
