public class AdminFlightDetailsViewModel
{
    public int FlightId { get; set; }
    public string FlightNumber { get; set; } = string.Empty;

    public string OriginAirport { get; set; } = string.Empty;
    public string DestinationAirport { get; set; } = string.Empty;

    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }

    public string AircraftModel { get; set; } = string.Empty;
    public int AircraftCapacity { get; set; }

    public decimal Price { get; set; }

    public int TotalBookedSeats { get; set; }
    public int TotalAvailableSeats { get; set; }
    public decimal TotalRevenue { get; set; }

    public List<AdminBookingInfo> Bookings { get; set; } = new();
}

public class AdminBookingInfo
{
    public int BookingId { get; set; }
    public string PassengerName { get; set; } = string.Empty;
    public string SeatNumber { get; set; } = string.Empty;
    public DateTime? BookingDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
