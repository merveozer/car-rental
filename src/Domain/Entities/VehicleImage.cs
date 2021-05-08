using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class VehicleImage
    {
        [Key]
        public int Id { get; set; }
        public int VehicleId { get; set; }

        [Display(Name = "Resim")]
        public string ImageUrl { get; set; }
    }
}
