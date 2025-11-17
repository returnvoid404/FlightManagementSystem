
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Models
{
    public class SQLFlightRepository : IFlightRepository
    {
        private readonly FlightManagementSystemContext context;

        public SQLFlightRepository(FlightManagementSystemContext context)
        {
            this.context = context;
        }

        public Flight AddFlight(Flight flight)
        {
            context.Flights.Add(flight);
            context.SaveChanges();
            return flight;
        }

        public IEnumerable<Flight> GetAllFlight()
        {
            return context.Flights
                .Include(f => f.Aircraft)
                .Include(f => f.OriginAirport)
                .Include(f => f.DestinationAirport)
                .ToList();
        }

        public Flight GetFlight(int flightId)
        {
            return context.Flights
                .Include(f => f.Aircraft)
                .Include(f => f.OriginAirport)
                .Include(f => f.DestinationAirport)
                .FirstOrDefault(f => f.FlightId == flightId);
        }

        public Flight RemoveFlight(int id)
        {
            Flight flight = context.Flights.Find(id);
            if (flight != null)
            {
                context.Flights.Remove(flight);
                context.SaveChanges();
            }
            return flight;
        }

        public Flight UpdateFlight(Flight flightChanges)
        {
            var flight = context.Flights.Attach(flightChanges);
            flight.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return flightChanges;
        }
    }

}
