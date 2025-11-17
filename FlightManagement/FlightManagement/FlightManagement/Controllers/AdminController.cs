using FlightManagement.Models;
using FlightManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IFlightRepository _flightRepository;
    private readonly FlightManagementSystemContext _context;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UserManager<ApplicationUser> userManager;

    public AdminController(IFlightRepository flightRepository, FlightManagementSystemContext context,
                            RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _flightRepository = flightRepository;
        _context = context;
        this.roleManager = roleManager;
        this.userManager = userManager;
    }
    public IActionResult Index()
    {
        var model = _flightRepository.GetAllFlight();
        return View(model);
    }
    [HttpGet]
    public IActionResult CreateRole()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
    {
        if (ModelState.IsValid)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = model.RoleName
            };
            IdentityResult result = await roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }
            foreach(IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult ListRoles() 
    {
        var roles = roleManager.Roles;
        return View(roles);
    }

    [HttpGet]
    public async Task<IActionResult> EditRole(string id)
    {
        var role = await roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return RedirectToAction("HttpStatusCodeHandler", "Error",
                new { statusCode = 404, message = $"The requested role with {id} could not be found." });
        }
        var model = new EditRoleViewModel
        {
            Id = role.Id,
            RoleName = role.Name,

        };
        var users = userManager.Users.ToList();

        foreach (var user in users)
        {
            if (await userManager.IsInRoleAsync(user, role.Name))
            {
                model.Users.Add(user.UserName);
            }
        }
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRole(EditRoleViewModel model)
    {
        var role = await roleManager.FindByIdAsync(model.Id);
        if (role == null)
        {
            return RedirectToAction("HttpStatusCodeHandler", "Error",
                new { statusCode = 404, message = $"The requested role with {model.Id} could not be found." });
        }
        else
        {
            role.Name = model.RoleName;
            var result = await roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        return View(model);
        }
    }

    [HttpGet]
    public IActionResult CreateFlight()
    {
        PopulateDropdowns();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateFlight(CreateFlightViewModel model)
    {
        if (ModelState.IsValid)
        {
            var flight = new Flight
            {
                FlightNumber = model.FlightNumber,
                OriginAirportId = model.OriginAirportId,
                DestinationAirportId = model.DestinationAirportId,
                AircraftId = model.AircraftId,
                DepartureTime = model.DepartureTime,
                ArrivalTime = model.ArrivalTime,
                Price = model.Price,
                Status = model.Status
            };

            _flightRepository.AddFlight(flight);
            return RedirectToAction("Index");
        }

        PopulateDropdowns();
        return View(model);
    }

    [HttpGet]
    public IActionResult EditFlight(int FlightId)
    {
        var flight = _flightRepository.GetFlight(FlightId);

        if (flight == null)
        {
            return StatusCode(404);
        }

        var editFlightViewModel = new EditFlightViewModel
        {
            FlightId = flight.FlightId,
            FlightNumber = flight.FlightNumber,
            OriginAirportId = flight.OriginAirportId,
            DestinationAirportId = flight.DestinationAirportId,
            AircraftId = flight.AircraftId,
            DepartureTime = flight.DepartureTime,
            ArrivalTime = flight.ArrivalTime,
            Price = flight.Price,
            Status = flight.Status
        };

        PopulateDropdowns();
        return View(editFlightViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditFlight(EditFlightViewModel model)
    {
        if (ModelState.IsValid)
        {
            var flight = new Flight
            {
                FlightId = model.FlightId,
                FlightNumber = model.FlightNumber,
                OriginAirportId = model.OriginAirportId,
                DestinationAirportId = model.DestinationAirportId,
                AircraftId = model.AircraftId,
                DepartureTime = model.DepartureTime,
                ArrivalTime = model.ArrivalTime,
                Price = model.Price,
                Status = model.Status
            };

            _flightRepository.UpdateFlight(flight);
            return RedirectToAction("Index");
        }

        PopulateDropdowns();
        return View(model);
    }

    [HttpPost]
    public IActionResult DeleteFlight(int FlightId)
    {
        var hasBookings = _context.Bookings.Any(b => b.FlightId == FlightId);

        if (hasBookings)
        {
            TempData["Error"] = "Cannot delete this flight because passengers have already booked it.";
            return RedirectToAction("Index");
        }

        _flightRepository.RemoveFlight(FlightId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> EditUsersInRole(string roleId)
    {
        ViewBag.RoleId = roleId;

        var role = await roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return RedirectToAction("HttpStatusCodeHandler", "Error",
                new { statusCode = 404, message = $"The requested role with ID {roleId} could not be found." });
        }

        var users = userManager.Users.ToList();

        var model = new List<UserRolesViewModel>();

        foreach (var user in users)
        {
            var userRolesViewModel = new UserRolesViewModel
            {
                UserID = user.Id,
                UserName = user.UserName,
                IsSelected = await userManager.IsInRoleAsync(user, role.Name)
            };

            model.Add(userRolesViewModel);
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditUsersInRole(List<UserRolesViewModel> model, string roleId)
    {
        var role = await roleManager.FindByIdAsync(roleId);
        if (role == null) {
            return RedirectToAction("HttpStatusCodeHandler", "Error",
                    new { statusCode = 404, message = $"The requested role with ID {roleId} could not be found." });
        }
        for (int i = 0; i < model.Count; i++) {
            var user =await userManager.FindByIdAsync(model[i].UserID);
            IdentityResult result = null;
            if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user,role.Name)))
            {
                result = await userManager.AddToRoleAsync(user, role.Name);
            }
            else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
            {
                result = await userManager.RemoveFromRoleAsync(user, role.Name);
            }
            else
            {
                continue;
            }
            if (result.Succeeded)
            {
                if (i < (model.Count - 1))
                {
                    continue;
                }
                else
                {
                    return RedirectToAction("EditRole", new { id = roleId });
                }
            }
        }
        return RedirectToAction("EditRole", new { id = roleId });
    }
    [HttpGet]
    public IActionResult Details(int FlightId)
    {
        var flight = _context.Flights
            .Include(f => f.Aircraft)
            .Include(f => f.OriginAirport)
            .Include(f => f.DestinationAirport)
            .Include(f => f.Bookings)
                .ThenInclude(b => b.Passenger)
            .FirstOrDefault(f => f.FlightId == FlightId);

        if (flight == null)
        {
            return RedirectToAction("HttpStatusCodeHandler", "Error",
                new { statusCode = 404, message = "Flight not found." });
        }

        // Calculate booked seats
        var bookedSeats = flight.Bookings.Select(b => b.SeatNumber).ToList();
        var availableSeats = Enumerable.Range(1, flight.Aircraft.Capacity)
            .Select(n => $"Seat-{n}")
            .Except(bookedSeats)
            .Count();

        var model = new AdminFlightDetailsViewModel
        {
            FlightId = flight.FlightId,
            FlightNumber = flight.FlightNumber,
            OriginAirport = flight.OriginAirport.AirportName,
            DestinationAirport = flight.DestinationAirport.AirportName,
            DepartureTime = flight.DepartureTime,
            ArrivalTime = flight.ArrivalTime,
            AircraftModel = $"{flight.Aircraft.Manufacturer} {flight.Aircraft.Model}",
            AircraftCapacity = flight.Aircraft.Capacity,
            Price = flight.Price,

            TotalBookedSeats = bookedSeats.Count,
            TotalAvailableSeats = availableSeats,
            TotalRevenue = bookedSeats.Count * flight.Price,

            Bookings = flight.Bookings.Select(b => new AdminBookingInfo
            {
                BookingId = b.BookingId,
                PassengerName = $"{b.Passenger.FirstName} {b.Passenger.LastName}",
                SeatNumber = b.SeatNumber,
                BookingDate = b.BookingDate,
                Email = b.Passenger.Email,
                Phone = b.Passenger.Phone
            }).ToList()
        };

        return View(model);
    }
    private void PopulateDropdowns()
    {
        ViewBag.Airports = _context.Airports.ToList();
        ViewBag.Aircrafts = _context.Aircrafts.ToList();
        ViewBag.StatusOptions = new List<string> { "Scheduled", "Delayed", "Cancelled", "Departed" };
    }
}
