﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Domain.Constants;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = AuthenticationConstants.AuthenticationScheme,
        Roles = AuthenticationConstants.OperationClaims.AdminStr)]
    public class VehicleRentalPriceController : Controller
    {
        private IVehicleRentalPriceService VehicleRentalPriceService { get; }
        private IVehicleService VehicleService { get; }
        private IRentalPeriodService RentalPeriodService { get; }

        public VehicleRentalPriceController(IVehicleRentalPriceService vehicleRentalPriceService,
                                            IVehicleService vehicleService,
                                            IRentalPeriodService rentalPeriodService)
        {
            VehicleRentalPriceService = vehicleRentalPriceService;
            VehicleService = vehicleService;
            RentalPeriodService = rentalPeriodService;
        }


        // GET: VehicleRentalPriceController
        public ActionResult Index(int vehicleId)
        {
            VehicleRentalPriceFilter filter = new VehicleRentalPriceFilter(vehicleId);
            var items = VehicleRentalPriceService.Get(filter);
            SetVehicleToViewBag(vehicleId);
            return View(items);
        }
        private void SetVehicleToViewBag(int vehicleId)
        {
            var vehicle = VehicleService.GetDetail(vehicleId);
            ViewBag.VehicleName = $"{vehicle?.VehicleBrandName} {vehicle?.VehicleModelName}";
            ViewBag.VehicleId = vehicleId;
        }

        // GET: VehicleRentalPriceController/Create
        public ActionResult Create(int vehicleId)
        {
            ViewBag.RentalPeriods = GetRentalPeriods();
            SetVehicleToViewBag(vehicleId);

            VehicleRentalPrice model = new VehicleRentalPrice();
            model.VehicleId = vehicleId;

            return View(model);
        }
        private List<SelectListItem> GetRentalPeriods()
        {
            return RentalPeriodService.Get(new RentalPeriodFilter()).Select(p => new SelectListItem(p.Name, p.Id.ToString())).ToList();
        }

        // POST: VehicleRentalPriceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehicleRentalPrice vehicleRentalPrice)
        {
            try
            {
                var response = VehicleRentalPriceService.Add(vehicleRentalPrice);
                ViewBag.Response = response;
            }
            catch
            {
                ViewBag.Response = Domain.DTOs.Response.Fail("Bir hata oluştu");
            }
            finally
            {
                SetVehicleToViewBag(vehicleRentalPrice.VehicleId);
                ViewBag.RentalPeriods = GetRentalPeriods();
            }
            return View(vehicleRentalPrice);
        }

        // GET: VehicleRentalPriceController/Edit/5
        public ActionResult Edit(int id)
        {
            var item = VehicleRentalPriceService.GetById(id);
            SetVehicleToViewBag(item.VehicleId);
            ViewBag.RentalPeriods = GetRentalPeriods();

            return View(item);
        }

        // POST: VehicleRentalPriceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VehicleRentalPrice vehicleRentalPrice)
        {
            try
            {
                var response = VehicleRentalPriceService.Update(vehicleRentalPrice);
                ViewBag.Response = response;
            }
            catch
            {
                ViewBag.Response = Domain.DTOs.Response.Fail("Bir hata oluştu");
            }
            finally
            {
                SetVehicleToViewBag(vehicleRentalPrice.VehicleId);
                ViewBag.RentalPeriods = GetRentalPeriods();
            }
            return View(vehicleRentalPrice);
        }

        // GET: VehicleRentalPriceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehicleRentalPriceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}