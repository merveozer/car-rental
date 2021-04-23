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
    public class ColorTypeService : BaseService, IColorTypeService
    {
        public ColorTypeService(ICarRentalDbContext context) : base(context)
        {

        }

        public Response Add(ColorType colorType)
        {
            Context.ColorType.Add(colorType);
            Context.SaveChanges();
            return Response.Success("Renk başarıyla kaydedildi.");
        }

        public Response Delete(int id)
        {
            var colorTypeToDelete = GetById(id);
            Context.ColorType.Remove(colorTypeToDelete);
            Context.SaveChanges();
            return Response.Success("Renk başarıyla silindi.");
        }

        public List<ColorType> Get(ColorTypeFilter filter)
        {
            var items = (from c in Context.ColorType
                         orderby c.Name
                         select c).ToList();
            return items;
        }

        public ColorType GetById(int id)
        {
            return Context.ColorType.Where(c => c.Id == id).SingleOrDefault();
        }

        public Response Update(ColorType colorType)
        {
            var colorTypeToUpdate = GetById(colorType.Id);
            colorTypeToUpdate.Name = colorType.Name;
            Context.SaveChanges();

            return Response.Success("Renk başarıyla güncellendi.");
        }
    }
}
