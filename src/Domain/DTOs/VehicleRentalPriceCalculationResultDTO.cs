namespace Domain.DTOs
{
    public class VehicleRentalPriceCalculationResultDTO
    {
        public int VehicleRentalPriceId { get; set; }
        public int NumberOfDays { get; set; }
        public decimal Amount { get; set; }
    }
}