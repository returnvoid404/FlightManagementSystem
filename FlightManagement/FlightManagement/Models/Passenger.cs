using System;
using System.Collections.Generic;

namespace FlightManagement.Models;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? PassportNumber { get; set; }

    public string UserId { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
