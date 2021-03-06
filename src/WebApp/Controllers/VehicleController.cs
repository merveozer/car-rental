using Application.Services;
using Application.Services.Concrete;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Controllers;
using WebApp.Models;
using Domain.Entities;
using Application.Extensions;

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
        private IVehicleImageService VehicleImageService { get; }
        private IVehicleRentalPriceService VehicleRentalPriceService { get; }
        private IVehicleRentalPriceCalculatorService VehicleRentalPriceCalculatorService {get;}
        private IRentVehicleService RentVehicleService { get; }

        public VehicleController(IVehicleService vehicleService,
                                 IVehicleModelService vehicleModelService,
                                 IVehicleClassTypeService vehicleClassTypeService,
                                 IColorTypeService colorTypeService,
                                 IFuelTypeService fuelTypeService,
                                 ITireTypeService tireTypeService,
                                 ITransmissionTypeService transmissionTypeService,
                                 IVehicleImageService vehicleImageService,
                                 IVehicleRentalPriceService vehicleRentalPriceService,
                                 IVehicleRentalPriceCalculatorService vehicleRentalPriceCalculatorService,
                                 IRentVehicleService rentVehicleService)
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
            VehicleRentalPriceCalculatorService = vehicleRentalPriceCalculatorService;
            RentVehicleService = rentVehicleService;
        }

    
        public IActionResult Index()
        {
            VehicleFilter filter = new VehicleFilter();
            var items = VehicleService.GetListItems(filter);
            ViewBag.Vehicles = items;
            SetParametersToViewBag();
            return View(filter);
        }

        [HttpPost]
        public IActionResult Index(VehicleFilter filter)
        {
            var items = VehicleService.GetListItems(filter);
            ViewBag.Vehicles = items;
            SetParametersToViewBag();
            return View(filter);
        }

        public IActionResult Detail(int id)
        {
            SetVehicleDetailToViewBag(id);

            RentVehicleDTO model = new RentVehicleDTO();
            model.VehicleId = id;
            return View("Detail", model);
        }

        [HttpPost]
        public IActionResult Calculate(RentVehicleDTO model)
        {

            var calculationResponse = VehicleRentalPriceCalculatorService.Calculate(model);
            if(calculationResponse.IsSuccess == false)
            {
                ViewBag.Response = Domain.DTOs.Response.Fail(calculationResponse.Message);
            }
            else
            {
                model.Amount =  calculationResponse.Data.Amount;
                model.VehicleRentalPriceId = calculationResponse.Data.VehicleRentalPriceId;
                model.NumberOfDays = calculationResponse.Data.NumberOfDays;

            }

            SetVehicleDetailToViewBag(model.VehicleId);
            return View("Detail", model);
        }

        [HttpPost]
        public IActionResult Rent(RentVehicleDTO model)
        {
            RentVehicle rentVehicle = new RentVehicle
            {
                Amount = model.Amount.Value,
                CreatedDate = DateTime.Now,
                DeliveryDate = model.DeliveryDate.Value,
                NumberOfDays = model.NumberOfDays,
                ReturnDate = model.ReturnDate.Value,
                VehicleId = model.VehicleId,
                VehicleRentalPriceId = model.VehicleRentalPriceId,
                UserId = HttpContext.User.GetUserId()
            };

            var rentResponse = RentVehicleService.Add(rentVehicle);
            ViewBag.Response = rentResponse;

            SetVehicleDetailToViewBag(model.VehicleId);
            return View("Detail", model);
        }

        private void SetVehicleDetailToViewBag(int id)
        {
            VehicleDetailViewModel vehicleDetail = new VehicleDetailViewModel();
            vehicleDetail.Vehicle = VehicleService.GetDetail(id);
            vehicleDetail.VehicleImages = VehicleImageService.GetByVehicle(id);
            vehicleDetail.VehicleRentalPrices = VehicleRentalPriceService.Get(new VehicleRentalPriceFilter(id, DateTime.Today));
            ViewBag.VehicleDetail = vehicleDetail;
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
    }
}