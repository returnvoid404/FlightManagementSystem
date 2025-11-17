using System;
using System.Collections.Generic;

namespace FlightManagement.Models;

public partial class Flight
{
    public int FlightId { get; set; }

    public string FlightNumber { get; set; } = null!;

    public int OriginAirportId { get; set; }

    public int DestinationAirportId { get; set; }

    public int AircraftId { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string Status { get; set; } = null!;
    public decimal Price { get; set; }

    public virtual Aircraft Aircraft { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Airport DestinationAirport { get; set; } = null!;

    public virtual Airport OriginAirport { get; set; } = null!;
}
