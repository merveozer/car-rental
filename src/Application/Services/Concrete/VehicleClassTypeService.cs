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
    public class VehicleClassTypeService : BaseService, IVehicleClassTypeService
    {
        public VehicleClassTypeService(ICarRentalDbContext context) : base(context)
        {
        }

        public Response Add(VehicleClassType vehicleClassType)
        {
            Context.VehicleClassType.Add(vehicleClassType);
            Context.SaveChanges();
            return Response.Success("Araç tipi başarıyla kaydedildi.");
        }

        public Response Delete(int id)
        {
            var vehicleClassTypeToDelete = GetById(id);
            Context.VehicleClassType.Remove(vehicleClassTypeToDelete);
            Context.SaveChanges();
            return Response.Success("Araç tipi başarıyla silindi.");
        }

        public List<VehicleClassType> Get(VehicleClassFilter filter)
        {
            var items = (from c in Context.VehicleClassType
                         orderby c.Name
                         select c).ToList();
            return items;
        }

        public VehicleClassType GetById(int id)
        {
            return Context.VehicleClassType.Where(c => c.Id == id).SingleOrDefault();

        }

        public Response Update(VehicleClassType vehicleClassType)
        {
            var vehicleClassTypeToUpdate = GetById(vehicleClassType.Id);
            vehicleClassTypeToUpdate.Name = vehicleClassType.Name;
            Context.SaveChanges();

            return Response.Success("Araç tipi başarıyla güncellendi.");
        }
    }
}
