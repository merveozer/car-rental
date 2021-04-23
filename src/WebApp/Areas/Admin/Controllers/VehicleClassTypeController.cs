﻿using Application.Services;
using Application.Services.Concrete;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    public class VehicleClassTypeController : Controller
    {
        public VehicleClassTypeController (IVehicleClassTypeService vehicleClassTypeService) 
        {
            VehicleClassTypeService = vehicleClassTypeService;
        }

        private IVehicleClassTypeService VehicleClassTypeService { get; }
        // GET: VehicleClassTypeController
        public ActionResult Index()
        {
            VehicleClassFilter filter = new VehicleClassFilter();
            var items = VehicleClassTypeService.Get(filter);
            return View(items);
        }

        // GET: VehicleClassTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VehicleClassTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleClassTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehicleClassType vehicleClassType)
        {
            try
            {
                var response = VehicleClassTypeService.Add(vehicleClassType);
                if (!response.IsSuccess)
                {
                    ViewBag.Response = response;
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleClassTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            var item = VehicleClassTypeService.GetById(id);
            return View(item);

        }

        // POST: VehicleClassTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VehicleClassType vehicleClassType)
        {
            try
            {
                var response = VehicleClassTypeService.Update(vehicleClassType);
                ViewBag.Response = response;
                return View(vehicleClassType);
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleClassTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            var item = VehicleClassTypeService.GetById(id);
            return View(item);
        }

        // POST: VehicleClassTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                VehicleClassTypeService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
