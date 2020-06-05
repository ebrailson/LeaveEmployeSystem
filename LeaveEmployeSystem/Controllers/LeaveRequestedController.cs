using AutoMapper;
using LeaveEmployeSystem.Data.Entities;
using LeaveEmployeSystem.Models.ViewModel;
using LeaveEmployeSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaveEmployeSystem.Controllers
{
    [Authorize]
    public class LeaveRequestedController : Controller
    {
        private readonly ILeaveRequestRepository _repoLeaveRequest;
        private readonly ILeaveAllocationRepository _repoLeaveAllocation;
        private readonly ILeaveTypeRepository _repoleaveType;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveRequestedController(
            ILeaveRequestRepository repoLeaveRequest,
            ILeaveAllocationRepository repoLeaveAllocation,
            ILeaveTypeRepository repoleaveType,
            IMapper mapper,
            UserManager<Employee> userManager
        )
        {
            _repoLeaveRequest = repoLeaveRequest;
            _repoLeaveAllocation = repoLeaveAllocation;
            _repoleaveType = repoleaveType;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admnistrator")]
        // GET:Adminstrator Index View
        public IActionResult Index()
        {
            var leaveRequest = _repoLeaveRequest.FindAll();
            var leaveRequestModel = _mapper.Map<List<LeaveRequestViewModel>>(leaveRequest);
            var model = new AdminViewRequestViewModel
            {
                TotalRequests = leaveRequestModel.Count,
                ApprovedRequests = leaveRequestModel.Count(l => l.Approved == true),
                PendingRequests = leaveRequestModel.Count(l => l.Approved == null),
                RejectedRequests = leaveRequestModel.Count(l => l.Approved == false),
                LeaveRequests = leaveRequestModel
            };

            return View(model);
        }
        public ActionResult Create()
        {
            var leaveTypes = _repoleaveType.FindAll();
            var leaveTyesItem = leaveTypes.Select(l => new SelectListItem
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });

            var model = new CreateLeaveRequestViewModel
            {
                LeaveTypes = leaveTyesItem
            };
            return View(model);
        }

        // POST: LeaveRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLeaveRequestViewModel model)
        {
            //var startDate = Convert.ToDateTime(model.StartDate);
            // var endDate = Convert.ToDateTime(model.EndDate);
            try
            {

                var leaveTypes = _repoleaveType.FindAll();
                // Get User current Log In
                var employee = _userManager.GetUserAsync(User).Result;
                // Check if his Request is valid and return one record of particular allocation 
                // that this Employye has for that Leave Type
                var allocation = _repoLeaveAllocation.GetLeaveAllocationByEmployeeAndType(employee.Id, model.LeaveTypeId);
                int daysRequested = (int)(model.EndDate - model.StartDate).TotalDays;
                var leaveTyesItem = leaveTypes.Select(l => new SelectListItem
                {
                    Text = l.Name,
                    Value = l.Id.ToString()
                });

                model.LeaveTypes = leaveTyesItem;
                if (allocation == null)
                {
                    ModelState.AddModelError("", "You Have No Days Left");
                }
                if (DateTime.Compare(model.StartDate, model.EndDate) > 1)
                {
                    ModelState.AddModelError("", "Start Date cannot be further in the future than the End Date");
                }
                //If the number of days requested exceed the number of days in the allocation 
                if (daysRequested > allocation.NumberOfDays)
                {
                    ModelState.AddModelError("", "You Do Not Sufficient Days For This Request");
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveRequestModel = new LeaveRequestViewModel
                {
                    RequestingEmployeeId = employee.Id,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Approved = null,
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    LeaveTypeId = model.LeaveTypeId,
                    RequestsComment = model.RequestsComment

                };

                var leaveRequest = _mapper.Map<LeaveRequested>(leaveRequestModel);

                var isSucces = _repoLeaveRequest.Create(leaveRequest);
                if (!isSucces)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }
                return RedirectToAction(nameof(MyLeave));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went very wrong");
                return View(model);
            }
        }

        // GET: LeaveRequest/Details/5
        public ActionResult Details(int id)
        {
            var leaveRequest = _repoLeaveRequest.FindById(id);
            var model = _mapper.Map<LeaveRequestViewModel>(leaveRequest);
            return View(model);
        }

        public IActionResult ApproveRequest(int id)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var leaveRequest = _repoLeaveRequest.FindById(id);
                var employeeId = leaveRequest.RequestingEmployeeId;
                var leaveTypeId = leaveRequest.LeaveTypeId;
                var allocation = _repoLeaveAllocation.GetLeaveAllocationByEmployeeAndType(employeeId, leaveTypeId);
                // Check if The number of days in the allcoation is less than the number of days in the request
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                allocation.NumberOfDays = allocation.NumberOfDays - daysRequested;

                leaveRequest.Approved = true;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;

                _repoLeaveRequest.Update(leaveRequest);
                _repoLeaveAllocation.Update(allocation);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));

            }
        }
        public IActionResult RejectRequest(int id)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var leaveRequest = _repoLeaveRequest.FindById(id);
                leaveRequest.Approved = false;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;

                _repoLeaveRequest.Update(leaveRequest);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));

            }

        }

        public IActionResult CancelRequest(int id)
        {
            var request = _repoLeaveRequest.FindById(id);
            request.Cancelled = true;
            _repoLeaveRequest.Update(request);
            return RedirectToAction("MyLeave");
        }

        public IActionResult MyLeave()
        {
            var employee = _userManager.GetUserAsync(User).Result;
            var employeeId = employee.Id;
            var employeeAllocation = _repoLeaveAllocation.GetLeaveAllocationById(employeeId);
            var employeeRequest = _repoLeaveRequest.GetLeaveRequestedByEmployee(employeeId);
            var employeeAllocationModel = _mapper.Map<List<LeaveAllocationViewModel>>(employeeAllocation);
            var employeeRequestModel = _mapper.Map<List<LeaveRequestViewModel>>(employeeRequest);
            var model = new EmployeeRequestViewModel
            {
                LeaveAllocations = employeeAllocationModel,
                LeaveRequests = employeeRequestModel
            };
            return View(model);

        }
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Delete/5
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
