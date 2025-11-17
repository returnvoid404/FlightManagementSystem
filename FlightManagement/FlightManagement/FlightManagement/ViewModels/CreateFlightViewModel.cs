namespace FlightManagement.ViewModels
{
    public class CreateFlightViewModel
    {
        public string FlightNumber { get; set; } = null!;
        public int OriginAirportId { get; set; }
        public int DestinationAirportId { get; set; }
        public int AircraftId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = "Scheduled";
    }
}
