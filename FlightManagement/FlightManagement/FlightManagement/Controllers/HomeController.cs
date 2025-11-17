using FlightManagement.Models;
using FlightManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly FlightManagementSystemContext _context;
        private readonly IFlightRepository _flightRepository;

        public HomeController(FlightManagementSystemContext context, IFlightRepository flightRepository)
        {
            this._context = context;
            _flightRepository = flightRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var flights = _flightRepository
                .GetAllFlight()
                .ToList();

            return View(flights);
        }
        [HttpGet]
        public IActionResult Details(int FlightId)
        {
            var flight = _flightRepository.GetFlight(FlightId);

            if (flight == null)
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", 
                    new { statusCode = 404, message = "The requested flight could not be found." });
            }
            var bookedSeats = _context.Bookings
            .Where(b => b.FlightId == FlightId)
            .Select(b => b.SeatNumber)
            .ToList();

            var availableSeats = Enumerable.Range(1, flight.Aircraft.Capacity)
                .Select(n => $"Seat-{n}")
                .Except(bookedSeats)
                .ToList();
            var model = new FlightDetailsViewModel
            {
                FlightId = FlightId,
                FlightNumber = flight.FlightNumber,
                OriginAirportName = flight.OriginAirport?.AirportName ?? "Unknown",
                DestinationAirportName = flight.DestinationAirport?.AirportName ?? "Unknown",
                AircraftModel = flight.Aircraft?.Model ?? "Unknown",
                AircraftCapacity = flight.Aircraft?.Capacity ?? 0,
                DepartureTime = flight.DepartureTime,
                ArrivalTime = flight.ArrivalTime,
                Status = flight.Status,
                Price = flight.Price,
                AvailableSeatsCount = availableSeats.Count
            };

            return View(model);
        }
    }
}
