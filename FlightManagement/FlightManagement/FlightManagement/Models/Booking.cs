using System;
using System.Collections.Generic;

namespace FlightManagement.Models;

public partial class Booking
{
    public int BookingId { get; set; }
    public int PassengerId { get; set; }
    public int FlightId { get; set; }
    public decimal Price { get; set; }
    public string SeatNumber { get; set; } = null!;
    public DateTime? BookingDate { get; set; }
    public virtual Flight Flight { get; set; } = null!;
    public virtual Passenger Passenger { get; set; } = null!;

}
