using Application.Infrastructure.Persistence;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Concrete
{
    public class VehicleBrandService : IVehicleBrandService
    {

        private ICarRentalDbContext Context { get; }
        public VehicleBrandService(CarRentalDbContext context)
        {
            Context = (ICarRentalDbContext)context;
        }
        public void Add(VehicleBrand vehicleBrand)
        {
            Context.VehicleBrand.Add(vehicleBrand);
            Context.SaveChanges();
        }

        public VehicleBrand GetById(int id)
        {
            return Context.VehicleBrand.Where(v => v.Id == id).SingleOrDefault();
        }

        public void Update (VehicleBrand vehicleBrand)
        {
            var vehicleBrandToUpdate = GetById(vehicleBrand.Id);
            vehicleBrandToUpdate.Name = vehicleBrand.Name;
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var vehicleBrandToDelete = GetById(id);
            Context.VehicleBrand.Remove(vehicleBrandToDelete);
            Context.SaveChanges();
        }

        public List<VehicleBrand>Get(VehicleBrandFilter vehicleBrandFilter)
        {
            var items = (from v in Context.VehicleBrand
                where v.Name.StartsWith(vehicleBrandFilter.Name)
                orderby v.Name
                select v).ToList();
            return items;
        }
        public string GetName()
        {
            return "Vehicle brand service";
        }
    }

}
