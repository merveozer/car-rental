using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class RentVehicleDTO
    {
        public int VehicleId { get; set; }

       [Display (Name = "Alış Tarihi")]
        public DateTime? DeliveryDate { get; set; }

        [Display (Name ="İade Tarihi")]
        public DateTime? ReturnDate { get; set; }

        [Display(Name ="Tutar")]
        public decimal? Amount { get; set; }


    }
}
