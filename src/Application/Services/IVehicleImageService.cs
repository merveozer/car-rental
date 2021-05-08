using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IVehicleImageService
    {
        Task<Response> Add(VehicleImage vehicleImage, IFormFile file);
        Response Delete(int id);
        List<VehicleImage> GetByVehicle(int id);

        VehicleImage GetById(int id);

        //public object VehicleImage { get; set; }


    }
}
