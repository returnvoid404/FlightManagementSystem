namespace FlightManagement.Models
{
    public interface IFlightRepository
    {
        Flight GetFlight(int FlightId);
        IEnumerable<Flight> GetAllFlight();
        Flight AddFlight(Flight flight);
        Flight UpdateFlight(Flight flightChanges);
        Flight RemoveFlight(int id);
    }
}
