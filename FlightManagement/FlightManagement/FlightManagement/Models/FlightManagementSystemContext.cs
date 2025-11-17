
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Models;

public partial class FlightManagementSystemContext : IdentityDbContext<ApplicationUser>
{
    public FlightManagementSystemContext()
    {
    }

    public FlightManagementSystemContext(DbContextOptions<FlightManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aircraft> Aircrafts { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-KN1BT3L;Database=FlightManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Aircraft>(entity =>
        {
            entity.HasKey(e => e.AircraftId).HasName("PK__Aircraft__F75CBC0BFCAACCC4");

            entity.Property(e => e.AircraftId).HasColumnName("AircraftID");
            entity.Property(e => e.Manufacturer).HasMaxLength(100);
            entity.Property(e => e.Model).HasMaxLength(100);
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.AirportId).HasName("PK__Airports__E3DBE08A382F544D");

            entity.HasIndex(e => e.AirportCode, "UQ__Airports__4B677353B40F1D41").IsUnique();

            entity.Property(e => e.AirportId).HasColumnName("AirportID");
            entity.Property(e => e.AirportCode).HasMaxLength(10);
            entity.Property(e => e.AirportName).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACDB7B0CACF");

            entity.HasIndex(e => new { e.FlightId, e.SeatNumber }, "UQ_Booking_Seat").IsUnique();

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.SeatNumber).HasMaxLength(10);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Flight).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Flight");

            entity.HasOne(d => d.Passenger).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PassengerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Passenger");

        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flights__8A9E148E0C496F82");

            entity.HasIndex(e => e.FlightNumber, "UQ__Flights__2EAE6F500ACAB17E").IsUnique();

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.AircraftId).HasColumnName("AircraftID");
            entity.Property(e => e.DestinationAirportId).HasColumnName("DestinationAirportID");
            entity.Property(e => e.FlightNumber).HasMaxLength(20);
            entity.Property(e => e.OriginAirportId).HasColumnName("OriginAirportID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Scheduled");

            entity.HasOne(d => d.Aircraft).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AircraftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flights_Aircarft");

            entity.HasOne(d => d.DestinationAirport).WithMany(p => p.FlightDestinationAirports)
                .HasForeignKey(d => d.DestinationAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flights_Destination");

            entity.HasOne(d => d.OriginAirport).WithMany(p => p.FlightOriginAirports)
                .HasForeignKey(d => d.OriginAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flights_Origin");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("PK__Passenge__88915F90D091FE8F");

            entity.HasIndex(e => e.PassportNumber, "UQ__Passenge__45809E71E05FDC70").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Passenge__A9D10534DB134758").IsUnique();

            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PassportNumber).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
