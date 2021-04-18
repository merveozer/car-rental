using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IVehicleBrandService
    {
        string GetName();
        void Add(VehicleBrand vehicleBrand);
        VehicleBrand GetById(int id);
        void Update(VehicleBrand vehicleBrand);
        void Delete(int id);
        List<VehicleBrand> Get(VehicleBrandFilter vehicleBrandFilter);



    }
}
