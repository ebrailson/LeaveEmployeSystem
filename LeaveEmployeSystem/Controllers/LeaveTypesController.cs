using AutoMapper;
using LeaveEmployeSystem.Data.Entities;
using LeaveEmployeSystem.Models.ViewModel;
using LeaveEmployeSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaveEmployeSystem.Controllers
{
    [Authorize(Roles = "Admnistrator")]
    public class LeaveTypesController : Controller
    {

        private readonly ILeaveTypeRepository _repo;
        private readonly IMapper _mapper;

        public LeaveTypesController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }


        // GET: LeaveTypes
        public ActionResult Index()
        {
            var leaveTypes = _repo.FindAll().ToList();
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeViewModel>>(leaveTypes);
            return View(model);
        }


        // GET: LeaveTypes/Details/5
        public ActionResult Details(int id)
        {
            if (!_repo.isExists(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeViewModel>(leaveType);

            return View(model);
        }


        // GET: LeaveTypes/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: LeaveTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;
                var isSucces = _repo.Create(leaveType);
                if (!isSucces)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(model);

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: LeaveTypes/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_repo.isExists(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeViewModel>(leaveType);
            return View(model);
        }

        // POST: LeaveTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveTypy = _mapper.Map<LeaveType>(model);
                var isSucces = _repo.Update(leaveTypy);
                if (!isSucces)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: LeaveTypes/Delete/5
        public ActionResult Delete(int id)
        {
            var leaveType = _repo.FindById(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            var isSucces = _repo.Delete(leaveType);
            if (!isSucces)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        //// POST: LeaveTypes/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, LeaveTypeViewModel model)
        //{
        //    try
        //    {
        //        var leaveType = _repo.FindById(id);
        //        if (leaveType == null)
        //        {
        //            return NotFound();
        //        }
        //        var isSucces = _repo.Delete(leaveType);
        //        if (!isSucces)
        //        {
        //            return View(model);
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        ModelState.AddModelError("", "Something went wrong...");
        //        return View(model);
        //    }
        //}
    }
}
