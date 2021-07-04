using Domain.Constants;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Concrete
{
    internal class VehicleRentalPriceCalculatorService : IVehicleRentalPriceCalculatorService
    {
        private IVehicleRentalPriceService VehicleRentalPriceService { get; }
        private IRentVehicleService RentVehicleService { get; }

        public VehicleRentalPriceCalculatorService(IVehicleRentalPriceService vehicleRentalPriceService,
                                                   IRentVehicleService rentVehicleService)
        {
            VehicleRentalPriceService = vehicleRentalPriceService;
            RentVehicleService = rentVehicleService;
        }

        public Response<VehicleRentalPriceCalculationResultDTO> Calculate(RentVehicleDTO rentVehicle)
        {
            var validationResponse = Validate(rentVehicle);
            if (validationResponse.IsSuccess == false)
                return validationResponse;

            var checkVehicleAvailability = CheckVehicleAvailability(rentVehicle.VehicleId,
                                                                    rentVehicle.DeliveryDate.Value,
                                                                    rentVehicle.ReturnDate.Value);
            if (checkVehicleAvailability.IsSuccess == false)
                return checkVehicleAvailability;

            int days = rentVehicle.ReturnDate.Value.Subtract(rentVehicle.DeliveryDate.Value).Days;
            var prices = VehicleRentalPriceService.Get(new VehicleRentalPriceFilter(rentVehicle.VehicleId, rentVehicle.DeliveryDate.Value));
            if (days < 30)
            {//Günlük
                return CalculateDaily(days, prices);
            }
            else if (days < 180)
            {//Aylık
                return CalculateMonthly(days, prices);
            }
            else
            {//6 Aylık
                return Calculate6Monthly(days, prices);
            }
        }

        private Response<VehicleRentalPriceCalculationResultDTO> CalculateDaily(int days, List<VehicleRentalPriceDTO> prices)
        {
            var vehicleRentalPrice = prices.Where(p => p.RentalPeriodId == RentalPeriodConstants.Gunluk).SingleOrDefault();
            if (vehicleRentalPrice == null)
                return Response<VehicleRentalPriceCalculationResultDTO>.Fail("Kiralama tarifesi bulunamadı");

            decimal amount = vehicleRentalPrice.Price * days;
            return Response<VehicleRentalPriceCalculationResultDTO>.Success(data: new VehicleRentalPriceCalculationResultDTO
            {
                Amount = amount,
                NumberOfDays = days,
                VehicleRentalPriceId = vehicleRentalPrice.Id
            });
        }
        private Response<VehicleRentalPriceCalculationResultDTO> CalculateMonthly(int days, List<VehicleRentalPriceDTO> prices)
        {
            var vehicleRentalPrice = prices.Where(p => p.RentalPeriodId == RentalPeriodConstants.Aylik).SingleOrDefault();
            if (vehicleRentalPrice == null)
                return CalculateDaily(days, prices);

            decimal amount = vehicleRentalPrice.Price * days;
            return Response<VehicleRentalPriceCalculationResultDTO>.Success(data: new VehicleRentalPriceCalculationResultDTO
            {
                Amount = amount,
                NumberOfDays = days,
                VehicleRentalPriceId = vehicleRentalPrice.Id
            });
        }
        private Response<VehicleRentalPriceCalculationResultDTO> Calculate6Monthly(int days, List<VehicleRentalPriceDTO> prices)
        {
            var vehicleRentalPrice = prices.Where(p => p.RentalPeriodId == RentalPeriodConstants._6_Aylik).SingleOrDefault();
            if (vehicleRentalPrice == null)
                return CalculateMonthly(days, prices);

            decimal amount = vehicleRentalPrice.Price * days;
            return Response<VehicleRentalPriceCalculationResultDTO>.Success(data: new VehicleRentalPriceCalculationResultDTO
            {
                Amount = amount,
                NumberOfDays = days,
                VehicleRentalPriceId = vehicleRentalPrice.Id
            });
        }

        private Response<VehicleRentalPriceCalculationResultDTO> Validate(RentVehicleDTO rentVehicle)
        {
            if (rentVehicle.DeliveryDate.HasValue == false || rentVehicle.ReturnDate.HasValue == false)
                return Response<VehicleRentalPriceCalculationResultDTO>.Fail("Lütfen tarihleri kontrol ediniz");
            if (rentVehicle.DeliveryDate.Value > rentVehicle.ReturnDate.Value)
                return Response<VehicleRentalPriceCalculationResultDTO>.Fail("İade tarihini alış tarihinden ileri bir tarih olarak seçiniz");
            if (rentVehicle.DeliveryDate.Value.Date <= DateTime.Today)
                return Response<VehicleRentalPriceCalculationResultDTO>.Fail("Lütfen alış tarihini en erken yarın olarak seçiniz");
            return Response<VehicleRentalPriceCalculationResultDTO>.Success();
        }
        private Response<VehicleRentalPriceCalculationResultDTO> CheckVehicleAvailability(int vehicleId,
                                                                                          DateTime deliveryDate,
                                                                                          DateTime returnDate)
        {
            var checkResponse = RentVehicleService.CheckVehicleAvailability(vehicleId, deliveryDate, returnDate);
            if (checkResponse.IsSuccess == false)
                return Response<VehicleRentalPriceCalculationResultDTO>.Fail(checkResponse.Message);
            return Response<VehicleRentalPriceCalculationResultDTO>.Success();
        }
    }
}