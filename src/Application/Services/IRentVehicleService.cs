using Domain.DTOs;
using Domain.Entities;
using System;

namespace Application.Services
{
    public interface IRentVehicleService
    {
        Response Add(RentVehicle rentVehicle);
        Response CheckVehicleAvailability(int vehicleId, DateTime deliveryDate, DateTime returnDate);
    }
}