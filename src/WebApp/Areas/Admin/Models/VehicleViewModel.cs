using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Models
{
    public class VehicleViewModel
    {
        public VehicleFilter Filter { get; set; }

        public List<VehicleDTO> Vehicles { get; set; }
    }
}
