using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    public class VehicleFilter
    {
        //[Display (Name = "") ]
        //public int VehicleBrandId { get; set; }

        [Display(Name = "Araç Modeli")]
        public int VehicleModelId { get; set; }

        [Display(Name = "Vites Tipi")]
        public int TransmissionTypeId { get; }

        [Display(Name = "Lastik Tipi")]
        public int TireTypeId { get; set; }

        [Display(Name = "Yakıt Tipi")]
        public int FuelTypeId { get; } = new int();

        [Display(Name = "Araç Tipi")]
        public int VehicleClassTypeId { get; }

        [Display(Name = "Renk")]
        public int ColorTypeId { get; } 
        public RangeValue<int?> ProductionYearRange { get; set; } = new RangeValue<int?>(); //soru işareti olduğundan nullable oldu
        public RangeValue<int?> EngineDisplacementRange { get; set; } = new RangeValue<int?>();
        public RangeValue<int?> HorsepowerRange { get; set; } = new RangeValue<int?>();
    }
}