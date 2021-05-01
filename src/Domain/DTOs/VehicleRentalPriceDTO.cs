using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    public class VehicleRentalPriceDTO
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public string VehicleBrandName { get; set; }

        public string VehicleModelName { get; set; }

        public int RentalPeriodId { get; set; }

        [Display(Name = "Kiralama Periyodu")]
        public string RentalPeriodName { get; set; }

        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        [Display(Name = "Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        public DateTime EndDate { get; set; }
    }
}