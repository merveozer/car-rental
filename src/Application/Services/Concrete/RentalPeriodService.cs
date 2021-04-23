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
    public class RentalPeriodService : BaseService, IRentalPeriodService
    {
        public RentalPeriodService(ICarRentalDbContext context) : base(context)
        {

        }

        public Response Add(RentalPeriod rentalPeriod)
        {
            Context.RentalPeriod.Add(rentalPeriod);
            Context.SaveChanges();
            return Response.Success("Kiralama dönemi başarıyla kaydedildi.");
        }

        public Response Delete(int id)
        {
            var rentalPeriodToDelete = GetById(id);
            Context.RentalPeriod.Remove(rentalPeriodToDelete);
            Context.SaveChanges();
            return Response.Success("Kiralama dönemi başarıyla silindi.");
        }

        public List<RentalPeriod> Get(RentalPeriodFilter filter)
        {
            var items = (from r in Context.RentalPeriod
                         orderby r.Name
                         select r).ToList();
            return items;
        }

        public RentalPeriod GetById(int id)
        {
            return Context.RentalPeriod.Where(r => r.Id == id).SingleOrDefault();
        }

        public Response Update(Domain.Entities.RentalPeriod rentalPeriod)
        {
            var rentalPeriodToUpdate = GetById(rentalPeriod.Id);
            rentalPeriodToUpdate.Name = rentalPeriod.Name;
            Context.SaveChanges();

            return Response.Success("Kiralama dönemi başarıyla güncellendi.");
        }
    }
}
