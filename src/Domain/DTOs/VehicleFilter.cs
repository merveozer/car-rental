using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    public class VehicleFilter
    {
        [Display (Name = "") ]
        public int? VehicleBrandId { get; set; }
        public int? VehicleModelId { get; set; }
        public int TransmissionTypeId { get; } 
        public int TireTypeId { get; set; }
        public int FuelTypeId { get; } = new int();
        public int VehicleClassTypeId { get; } 
        public int ColorTypeId { get; } 
        public RangeValue<int?> ProductionYearRange { get; set; } = new RangeValue<int?>(); //soru işareti olduğundan nullable oldu
        public RangeValue<int?> EngineDisplacementRange { get; set; } = new RangeValue<int?>();
        public RangeValue<int?> HorsepowerRange { get; set; } = new RangeValue<int?>();
    }
}