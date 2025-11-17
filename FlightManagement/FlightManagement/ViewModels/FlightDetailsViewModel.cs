namespace FlightManagement.ViewModels
{
    public class FlightDetailsViewModel
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = null!;
        public string OriginAirportName { get; set; } = null!;
        public string DestinationAirportName { get; set; } = null!;
        public string AircraftModel { get; set; } = null!;
        public int AircraftCapacity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Status { get; set; } = null!; 
        public decimal Price { get; set; }

        // Optional: List of booked seats for display
        public int AvailableSeatsCount { get; set; }
    }
}
