using AutoMapper;
using LeaveEmployeSystem.Data.Entities;
using LeaveEmployeSystem.Models.ViewModel;
using LeaveEmployeSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaveEmployeSystem.Controllers
{
    [Authorize(Roles = "Admnistrator")]
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveAllocationRepository _repoLeaveAllocation;
        private readonly ILeaveTypeRepository _repoLeaveType;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        public LeaveAllocationController(ILeaveAllocationRepository repoLeaveAllocation,
            ILeaveTypeRepository repoLeaveType,
            IMapper mapper,
           UserManager<Employee> userManager)
        {
            _mapper = mapper;
            _repoLeaveAllocation = repoLeaveAllocation;
            _repoLeaveType = repoLeaveType;
            _userManager = userManager;
        }
        // GET: LeaveAlloctaion
        public ActionResult Index()
        {
            var leaveTypes = _repoLeaveType.FindAll().ToList();
            var mappedLeaveType = _mapper.Map<List<LeaveType>, List<LeaveTypeViewModel>>(leaveTypes);
            var model = new CreateLeaveAllocationTypeViewModel
            {
                LeaveType = mappedLeaveType,
                NumberUpdated = 0
            };
            return View(model);
        }

        public IActionResult AllocateLeave(int id)
        {
            var leaveType = _repoLeaveType.FindById(id);
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;

            foreach (var emp in employees)
            {
                if (_repoLeaveAllocation.CheckExistsAllocation(id, emp.Id))
                    continue;
                var allocation = new LeaveAllocationViewModel
                {
                    DateCreated = DateTime.Now,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = DateTime.Now.Year,
                    LeaveTypeId = id,
                    EmployeeId = emp.Id

                };
                var leaveAllocation = _mapper.Map<LeaveAllocation>(allocation);
                _repoLeaveAllocation.Create(leaveAllocation);

            }
            return RedirectToAction(nameof(Index));

        }

        public IActionResult ListEmployees()
        {
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            var model = _mapper.Map<List<EmployeeViewModel>>(employees);
            return View(model);
        }
        // GET: LeaveAlloctaion/Details/5
        public IActionResult Details(string id)
        {
            var getEmployee = _userManager.FindByIdAsync(id).Result;
            var employee = _mapper.Map<EmployeeViewModel>(getEmployee);
            var allocations = _mapper.Map<List<LeaveAllocationViewModel>>(_repoLeaveAllocation.GetLeaveAllocationById(id));
            var model = new ViewAllocationViewModel
            {
                Employee = employee,
                LeaveAllocations = allocations
            };
            return View(model);
        }

        // GET: LeaveAlloctaion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAlloctaion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAlloctaion/Edit/5
        public ActionResult Edit(int id)
        {
            var leaveAllocation = _mapper.Map<EditLeaveAllocationTypeViewModel>(_repoLeaveAllocation.FindById(id));

            return View(leaveAllocation);
        }

        // POST: LeaveAlloctaion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditLeaveAllocationTypeViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // Find the original data
                var record = _repoLeaveAllocation.FindById(model.Id);

                //I have to mapped manually, if not it doesnt work,because it is just one field we want to update
                record.NumberOfDays = model.NumberOfDays;

                //Optional
                var leaveAllocation = _mapper.Map<LeaveAllocation>(record);

                var isSucces = _repoLeaveAllocation.Update(leaveAllocation);

                if (!isSucces)
                {
                    ModelState.AddModelError("", "Something go worng");
                    return View(model);
                }

                return RedirectToAction(nameof(Details), new { id = model.EmployeeId });
            }
            catch
            {
                return View(model);
            }
        }

        // GET: LeaveAlloctaion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAlloctaion/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
