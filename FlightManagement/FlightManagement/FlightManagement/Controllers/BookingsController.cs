using FlightManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class BookingsController : Controller
{
    private readonly FlightManagementSystemContext _context;

    public BookingsController(FlightManagementSystemContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create(int flightId)
    {
        var flight = _context.Flights
            .Include(f => f.Aircraft)
            .FirstOrDefault(f => f.FlightId == flightId);

        if (flight == null)
        {
            return RedirectToAction("HttpStatusCodeHandler", "Error",
                new { statusCode = 404, message = "Flight not found." });
        }

        var bookedSeats = _context.Bookings
            .Where(b => b.FlightId == flightId)
            .Select(b => b.SeatNumber)
            .ToList();

        var availableSeats = Enumerable.Range(1, flight.Aircraft.Capacity)
            .Select(n => $"Seat-{n}")
            .Except(bookedSeats)
            .ToList();

        var model = new BookingViewModel
        {
            FlightId = flight.FlightId,
            FlightNumber = flight.FlightNumber,
            AvailableSeats = availableSeats,
            Price = flight.Price
        };

        // Get potential return flights (same route, reverse)
        var returnFlights = _context.Flights
            .Where(f => f.OriginAirportId == flight.DestinationAirportId
                     && f.DestinationAirportId == flight.OriginAirportId
                     && f.DepartureTime > flight.DepartureTime)
            .Select(f => new SelectListItem
            {
                Value = f.FlightId.ToString(),
                Text = $"{f.FlightNumber} | {f.Aircraft.Model} | {f.DepartureTime:f}"
            })
            .ToList();

        return View(model);
    }
    [HttpPost]
    public IActionResult Create(BookingViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Reload seats & return flights if validation fails
            var flight = _context.Flights
                .Include(f => f.Aircraft)
                .FirstOrDefault(f => f.FlightId == model.FlightId);
            if (flight != null)
            {
                var bookedSeats = _context.Bookings
                    .Where(b => b.FlightId == flight.FlightId)
                    .Select(b => b.SeatNumber)
                    .ToList();

                model.AvailableSeats = Enumerable.Range(1, flight.Aircraft.Capacity)
                    .Select(n => $"Seat-{n}")
                    .Except(bookedSeats)
                    .ToList();

                model.Price = flight.Price;
            }
            return View(model);
        }
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
        // Create or get passenger
        var passenger = _context.Passengers 
            .FirstOrDefault(p => p.Email == model.Email);

        if (passenger == null)
        {
            passenger = new Passenger
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                PassportNumber = model.PassportNumber,
                UserId = userId
            };
            _context.Passengers.Add(passenger);
            _context.SaveChanges();
        }

        var flightDetails = _context.Flights
            .Include(f => f.Aircraft)
            .FirstOrDefault(f => f.FlightId == model.FlightId);

        if (flightDetails == null)
            return RedirectToAction("HttpStatusCodeHandler", "Error");
        model.Price = flightDetails.Price;
        // Create booking
        var booking = new Booking
        {
            PassengerId = passenger.PassengerId,
            FlightId = model.FlightId,
            SeatNumber = model.SeatNumber,
            BookingDate = DateTime.Now,
            Price = model.Price,
        };

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        return RedirectToAction("Confirmation", new { id = booking.BookingId });
    }


    [HttpGet]
    public IActionResult Confirmation(int id)
    {
        var booking = _context.Bookings
            .Include(b => b.Flight)
                .ThenInclude(f => f.Aircraft)
            .Include(b => b.Passenger)
            .FirstOrDefault(b => b.BookingId == id);

        if (booking == null)
            return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 404, message = "Booking not found." });

        return View(booking);
    }

    [HttpGet]
    public IActionResult MyBookings()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        var bookings = _context.Bookings
            .Include(b => b.Flight)
                .ThenInclude(f => f.Aircraft)
            .Include(b => b.Flight)
                .ThenInclude(f => f.OriginAirport)
            .Include(b => b.Flight)
                .ThenInclude(f => f.DestinationAirport)
            .Include(b => b.Passenger)
            .Where(b => b.Passenger.UserId == userId)
            .OrderByDescending(b => b.BookingDate)
            .ToList();

        return View(bookings);
    }
}
