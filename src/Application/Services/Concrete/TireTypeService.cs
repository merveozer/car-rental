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
    public class TireTypeService : BaseService, ITireTypeService
    {
        public TireTypeService(ICarRentalDbContext context) : base(context)
        {
        }

        public Response Add(TireType tireType)
        {
            Context.TireType.Add(tireType);
            Context.SaveChanges();
            return Response.Success("Tekerlek tipi başarıyla kaydedildi."); 
        }

        public Response Delete(int id)
        {
            var tireTypeToDelete = GetById(id);
            Context.TireType.Remove(tireTypeToDelete);
            Context.SaveChanges();

            return Response.Success("Tekerlek tipi başarıyla silindi.");
        }

        public List<TireType> Get(TireTypeFilter filter)
        {
            var items = (from t in Context.TireType
                         orderby t.Name
                         select t).ToList();
            return items;
        }

        public TireType GetById(int id)
        {
            return Context.TireType.Where(t => t.Id == id).SingleOrDefault();

        }

        public Response Update(TireType tireType)
        {
            var tireTypeToUpdate = GetById(tireType.Id);
            tireTypeToUpdate.Name = tireType.Name;
            Context.SaveChanges();

            return Response.Success("Tekerlek tipi başarıyla güncellendi.");
        }
    }
}
