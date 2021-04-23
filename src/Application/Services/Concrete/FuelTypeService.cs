using Application.Infrastructure.Persistence;
using Application.Services.Common;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Concrete
{
    public class FuelTypeService : BaseService, IFuelTypeService
    {
        public FuelTypeService(ICarRentalDbContext context) : base(context)
        {

        }

        public Response Add(FuelType fuelType)
        {
            Context.FuelType.Add(fuelType);
            Context.SaveChanges();
            return Response.Success("Yakıt tipi başarıyla kaydedildi.");
        }

        public Response Delete(int id)
        {
            var fuelTypeToDelete = GetById(id);
            Context.FuelType.Remove(fuelTypeToDelete);
            Context.SaveChanges();
            return Response.Success("Yakıt tipi başarıyla silindi.");
        }

        public List<FuelType> Get(FuelTypeFilter filter)
        {
            var items = (from f in Context.FuelType
                         orderby f.Name
                         select f).ToList();
            return items;
        }

        public FuelType GetById(int id)
        {
            return Context.FuelType.Where(f => f.Id == id).SingleOrDefault();
        }

        public Response Update(FuelType fuelType)
        {
            var fuelTypeToUpdate = GetById(fuelType.Id);
            fuelTypeToUpdate.Name = fuelType.Name;
            Context.SaveChanges();

            return Response.Success("Yakıt tipi başarıyla güncellendi.");
        }
    }
}
