﻿
using Domain.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Services
{
    public interface IVehicleBrandService
    {
        Response Add(VehicleBrand vehicleBrand);
        Response Update(VehicleBrand vehicleBrand);
        Response Delete(int id);
        VehicleBrand GetById(int id);
        List<VehicleBrand> Get(VehicleBrandFilter filter);

        string GetName();
    }
}