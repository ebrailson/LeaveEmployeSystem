using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveEmployeSystem.Models.ViewModel
{
    public class LeaveTypeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }
    }

}
