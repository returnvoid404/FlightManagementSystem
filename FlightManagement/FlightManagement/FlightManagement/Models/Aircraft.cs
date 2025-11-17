using System;
using System.Collections.Generic;

namespace FlightManagement.Models;

public partial class Aircraft
{
    public int AircraftId { get; set; }

    public string Model { get; set; } = null!;

    public string? Manufacturer { get; set; }

    public int Capacity { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
