﻿using Application.Services;
using Application.Services.Concrete;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class VehicleController : Controller
    {
        private IVehicleService VehicleService { get; }
        private IVehicleModelService VehicleModelService { get; }
        private IVehicleClassTypeService VehicleClassTypeService { get; }
        private IColorTypeService ColorTypeService { get; }
        private IFuelTypeService FuelTypeService { get; }
        private ITireTypeService TireTypeService { get; }
        private ITransmissionTypeService TransmissionTypeService { get; }
        private IVehicleRentalPriceService VehicleRentalPriceService {get;}
        private IVehicleImageService VehicleImageService { get; }

        public VehicleController(IVehicleService vehicleService,
                                 IVehicleModelService vehicleModelService,
                                 IVehicleClassTypeService vehicleClassTypeService,
                                 IColorTypeService colorTypeService,
                                 IFuelTypeService fuelTypeService,
                                 ITireTypeService tireTypeService,
                                 ITransmissionTypeService transmissionTypeService,
                                 IVehicleImageService vehicleImageService,
                                 IVehicleRentalPriceService vehicleRentalPriceService
                                 )
        {
            VehicleService = vehicleService;
            VehicleModelService = vehicleModelService;
            VehicleClassTypeService = vehicleClassTypeService;
            ColorTypeService = colorTypeService;
            FuelTypeService = fuelTypeService;
            TireTypeService = tireTypeService;
            TransmissionTypeService = transmissionTypeService;
            VehicleImageService = vehicleImageService;
            VehicleRentalPriceService = vehicleRentalPriceService;

        }

        public IActionResult Detail(int id)
        {
            VehicleDetailViewModel vehicleDetail = new VehicleDetailViewModel();
            vehicleDetail.Vehicle = VehicleService.GetDetail(id);
            vehicleDetail.VehicleImages = VehicleImageService.GetByVehicle(id);
            vehicleDetail.VehicleRentalPrices = VehicleRentalPriceService.Get(new VehicleRentalPriceFilter(id, DateTime.Today));
            ViewBag.VehicleDetail = vehicleDetail;
            return View();
        }
        private void SetParametersToViewBag()
        {
            ViewBag.VehicleModels = GetVehicleModels();
            ViewBag.VehicleClassTypes = GetVehicleClassTypes();
            ViewBag.ColorTypes = GetColorTypes();
            ViewBag.FuelTypes = GetFuelTypes();
            ViewBag.TireTypes = GetTireTypes();
            ViewBag.TransmissionTypes = GetTransmissionTypes();
        }
        private List<SelectListItem> GetVehicleModels()
        {
            return VehicleModelService.Get(new VehicleModelFilter()).Select(m => new SelectListItem($"{m.VehicleBrandName} {m.Name}", m.Id.ToString())).ToList();
        }
        private List<SelectListItem> GetVehicleClassTypes()
        {
            return VehicleClassTypeService.Get(new VehicleClassFilter()).Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
        }
        private List<SelectListItem> GetColorTypes()
        {
            return ColorTypeService.Get(new ColorTypeFilter()).Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
        }
        private List<SelectListItem> GetFuelTypes()
        {
            return FuelTypeService.Get(new FuelTypeFilter()).Select(f => new SelectListItem(f.Name, f.Id.ToString())).ToList();
        }
        private List<SelectListItem> GetTireTypes()
        {
            return TireTypeService.Get(new TireTypeFilter()).Select(t => new SelectListItem(t.Name, t.Id.ToString())).ToList();
        }
        private List<SelectListItem> GetTransmissionTypes()
        {
            return TransmissionTypeService.Get(new TransmissionTypeFilter()).Select(t => new SelectListItem(t.Name, t.Id.ToString())).ToList();
        }
    
        public IActionResult Index()
        {
            VehicleFilter filter = new VehicleFilter();
            var items = VehicleService.GetListItems(filter);
            ViewBag.Vehicles = items;
            SetParametersToViewBag();
            return View(filter);
            
        }

        [HttpPost] //post işleminde hangi verileri almam lazım ne göndermem lazım bu sorunun cevabı modelini belirler
        public IActionResult Index(VehicleFilter filter)
        {
            var items = VehicleService.GetListItems(filter);
            ViewBag.Vehicles = items;
            return View(filter);
        }

    }
}
