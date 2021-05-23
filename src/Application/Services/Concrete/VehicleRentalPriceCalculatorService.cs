﻿using Domain.Constants;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Concrete
{
    internal class VehicleRentalPriceCalculatorService : IVehicleRentalPriceCalculatorService
    {
        private IVehicleRentalPriceService VehicleRentalPriceService { get; }

        public VehicleRentalPriceCalculatorService(IVehicleRentalPriceService vehicleRentalPriceService)
        {
            VehicleRentalPriceService = vehicleRentalPriceService;
        }

        public Response<decimal> Calculate(RentVehicleDTO rentVehicle)
        {
            var validationResponse = Validate(rentVehicle);
            if (validationResponse.IsSuccess == false)
                return validationResponse;

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

        private Response<decimal> CalculateDaily(int days, List<VehicleRentalPriceDTO> prices)
        {
            var vehicleRentalPrice = prices.Where(p => p.RentalPeriodId == RentalPeriodConstants.Gunluk).SingleOrDefault();
            if (vehicleRentalPrice == null)
                return Response<decimal>.Fail("Kiralama tarifesi bulunamadı");

            decimal amount = vehicleRentalPrice.Price * days;
            return Response<decimal>.Success(data: amount);
        }
        private Response<decimal> CalculateMonthly(int days, List<VehicleRentalPriceDTO> prices)
        {
            var vehicleRentalPrice = prices.Where(p => p.RentalPeriodId == RentalPeriodConstants.Aylik).SingleOrDefault();
            if (vehicleRentalPrice == null)
                return CalculateDaily(days, prices);

            decimal amount = vehicleRentalPrice.Price * days;
            return Response<decimal>.Success(data: amount);
        }
        private Response<decimal> Calculate6Monthly(int days, List<VehicleRentalPriceDTO> prices)
        {
            var vehicleRentalPrice = prices.Where(p => p.RentalPeriodId == RentalPeriodConstants._6_Aylik).SingleOrDefault();
            if (vehicleRentalPrice == null)
                return CalculateMonthly(days, prices);

            decimal amount = vehicleRentalPrice.Price * days;
            return Response<decimal>.Success(data: amount);
        }

        private Response<decimal> Validate(RentVehicleDTO rentVehicle)
        {
            if (rentVehicle.DeliveryDate.HasValue == false || rentVehicle.ReturnDate.HasValue == false)
                return Response<decimal>.Fail("Lütfen tarihleri kontrol ediniz");
            if (rentVehicle.DeliveryDate.Value > rentVehicle.ReturnDate.Value)
                return Response<decimal>.Fail("İade tarihini alış tarihinden ileri bir tarih olarak seçiniz");
            if (rentVehicle.DeliveryDate.Value.Date <= DateTime.Today)
                return Response<decimal>.Fail("Lütfen alış tarihini en erken yarın olarak seçiniz");
            return Response<decimal>.Success();
        }
    }
}