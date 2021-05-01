using Application.Services.Concrete;
using Domain.DTOs;
using System;
using Domain.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Services
{
    public interface IVehicleRentalPriceService
    {
        Response Add(VehicleRentalPrice vehicleRentalPrice);
        Response Update(VehicleRentalPrice vehicleRentalPrice);
        Response Delete(int id);
        VehicleRentalPrice GetById(int id);
        VehicleRentalPriceDTO GetDetail(int id);
        List<VehicleRentalPriceDTO> Get(VehicleRentalPriceFilter filter);
    }
}