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
    public class TransmissionTypeService : BaseService, ITransmissionTypeService
    {
        public TransmissionTypeService(ICarRentalDbContext context) : base(context)
        {

        }

        public Response Add(TransmissionType transmissionType)
        {
            Context.TransmissionType.Add(transmissionType);
            Context.SaveChanges();
            return Response.Success("Vites tipi başarıyla kaydedildi.");
        }

        public Response Delete(int id)
        {
            var transmissionTypeToDelete = GetById(id);
            Context.TransmissionType.Remove(transmissionTypeToDelete);
            Context.SaveChanges();
            return Response.Success("Vites tipi başarıyla silindi.");
        }

        public List<TransmissionType> Get(TransmissionTypeFilter filter)
        {
            var items = (from t in Context.TransmissionType
                         orderby t.Name
                         select t).ToList();
            return items;
        }

        public TransmissionType GetById(int id)
        {
            return Context.TransmissionType.Where(t => t.Id == id).SingleOrDefault();
        }

        public Response Update(TransmissionType transmissionType)
        {
            var transmissionTypeToUpdate = GetById(transmissionType.Id);
            transmissionTypeToUpdate.Name = transmissionType.Name;
            Context.SaveChanges();

            return Response.Success("Vites tipi başarıyla güncellendi.");
        }
    }
}
