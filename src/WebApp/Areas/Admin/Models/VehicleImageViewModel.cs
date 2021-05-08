using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class VehicleImageViewModel
    {
        [Display ( Name = "Araç")]
        public int VehicleId { get; set; }


        [Display(Name = "Resim")]
        public IFormFile Image { get; set; }
    }
}
