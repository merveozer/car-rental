using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class VehicleRentalPrice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Araç")]
        public int VehicleId { get; set; }

        [Required]
        [Display(Name = "Kiralama Periyodu")]
        public int RentalPeriodId { get; set; }

        [Required]
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Bitiş Tarihi")]
        public DateTime EndDate { get; set; }

        public Vehicle Vehicle { get; set; }
        public RentalPeriod RentalPeriod { get; set; }
    }
}