using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Concrete
{
   public interface IFuelTypeService
    {
        Response Add(FuelType fuelType);
        Response Update(FuelType fuelType);
        Response Delete(int id);
        FuelType GetById(int id);
        List<FuelType> Get(FuelTypeFilter filter);
    }
}
