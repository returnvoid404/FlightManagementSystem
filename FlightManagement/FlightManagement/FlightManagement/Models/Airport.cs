using System;
using System.Collections.Generic;

namespace FlightManagement.Models;

public partial class Airport
{
    public int AirportId { get; set; }

    public string AirportCode { get; set; } = null!;

    public string AirportName { get; set; } = null!;

    public string? City { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Flight> FlightDestinationAirports { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightOriginAirports { get; set; } = new List<Flight>();
}
