using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class BookingViewModel
{
    public int FlightId { get; set; }
    public string FlightNumber { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Seat Number")]
    public string SeatNumber { get; set; } = string.Empty;

    public List<string> AvailableSeats { get; set; } = new List<string>();

    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Phone { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "Passport Number")]
    public string PassportNumber { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
