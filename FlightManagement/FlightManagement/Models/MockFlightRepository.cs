
namespace FlightManagement.Models
{
    public class MockFlightRepository
    {
        //private List<Flight> _flightList;

        //public MockFlightRepository()
        //{
        //    _flightList = new List<Flight>()
        //    {
        //        new Flight() { flight_Number = "AZN0021", source = "Karachi", Destination = "Islamabad", departure_Time = new TimeOnly(15, 30), arrival_Time = new TimeOnly(17, 30), departure_Date = new DateOnly(2025,12,9), arrival_Date = new DateOnly(2025,12,9), ticket_Price=23240},
        //        new Flight() { flight_Number = "AZN0022", source = "Karachi", Destination = "Islamabad", departure_Time = new TimeOnly(15, 30), arrival_Time = new TimeOnly(17, 30), departure_Date = new DateOnly(2025,12,9), arrival_Date = new DateOnly(2025,12,9), ticket_Price=23240},
        //        new Flight() { flight_Number = "AZN0023", source = "Karachi", Destination = "Islamabad", departure_Time = new TimeOnly(15, 30), arrival_Time = new TimeOnly(17, 30), departure_Date = new DateOnly(2025,12,9), arrival_Date = new DateOnly(2025,12,9), ticket_Price=23240},
        //    };
        //}

        //public Flight AddFlight(Flight flight)
        //{
        //    string prefix = "AZN";
        //    int nextNumber = 1;

        //    if (_flightList.Any())
        //    {
        //        // Get the max numeric part from existing flight numbers
        //        var lastFlight = _flightList
        //            .OrderByDescending(f => int.Parse(f.flight_Number.Substring(prefix.Length)))
        //            .First();

        //        int lastNumber = int.Parse(lastFlight.flight_Number.Substring(prefix.Length));
        //        nextNumber = lastNumber + 1;
        //    }

        //    // Format as AZN00021 style (5 digits)
        //    flight.flight_Number = $"{prefix}{nextNumber:D5}";

        //    _flightList.Add(flight);
        //    return flight;
        //}


        //public IEnumerable<Flight> GetAllFlight()
        //{
        //    return _flightList;
        //}

        //public Flight GetFlight(string flight_Number)
        //{
        //    return _flightList.FirstOrDefault(e => e.flight_Number.Equals(flight_Number));
        //}

        //public Flight RemoveFlight(int id)
        //{
        //    Flight flight = _flightList.FirstOrDefault(e => e.Id == id);
        //    if (flight != null) { 
        //    _flightList.Remove(flight);
        //    }
        //    return flight;
        //}

        //public Flight UpdateFlight(Flight flightChanges)
        //{
        //    Flight flight = _flightList.FirstOrDefault(e => e.Id == flightChanges.id);
        //    if (flight != null)
        //    {
        //        _flightList.flightId = flightChanges.FlightId;
        //    }
        //    return flight;
        //}
    }
}
