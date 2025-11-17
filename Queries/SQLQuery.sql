CREATE DATABASE FlightManagementSystem;
GO

USE FlightManagementSystem;

CREATE TABLE Airports(
	AirportID INT IDENTITY(1,1) PRIMARY KEY,
	AirportCode NVARCHAR(10) NOT NULL UNIQUE,
	AirportName NVARCHAR(100) NOT NULL,
	City NVARCHAR(100),
	Country NVARCHAR(100),
);

CREATE TABLE Aircrafts(
	AircraftID INT IDENTITY(1,1) PRIMARY KEY,
	Model NVARCHAR(100) NOT NULL,
	Manufacturer NVARCHAR(100),
	Capacity INT NOT NULL,
);

CREATE TABLE Flights(
	FlightID INT IDENTITY(1,1) PRIMARY KEY,
	FlightNumber NVARCHAR(20) NOT NULL UNIQUE,
	OriginAirportID INT NOT NULL,
	DestinationAirportID INT NOT NULL,
	AircraftID INT NOT NULL,
	DepartureTime DATETIME2 NOT NULL,
	ArrivalTime DATETIME2 NOT NULL,
	Status NVARCHAR(50) NOT NULL DEFAULT 'Scheduled',

	CONSTRAINT FK_Flights_Origin 
	FOREIGN KEY (OriginAirportID) REFERENCES Airports(AirportID),
	
	CONSTRAINT FK_Flights_Destination 
	FOREIGN KEY (DestinationAirportID) REFERENCES Airports(AirportID),

	CONSTRAINT FK_Flights_Aircarft 
	FOREIGN KEY (AircraftID) REFERENCES Aircrafts(AircraftID),

	CONSTRAINT CK_Flight_Origin_Destination CHECK (OriginAirportID <> DestinationAirportID),
);

CREATE TABLE Passengers(
	PassengerID INT IDENTITY(1,1) PRIMARY KEY,
	FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100),
	Email NVARCHAR(150) UNIQUE,
	Phone NVARCHAR(20),
	PassportNumber NVARCHAR(50) UNIQUE,
);

CREATE TABLE Bookings(
	BookingID INT IDENTITY(1,1) PRIMARY KEY,
	PassengerID INT NOT NULL,
	FlightID INT NOT NULL,
	SeatNumber NVARCHAR(10) NOT NULL,
	Price DECIMAL(10, 2) NOT NULL,
	BookingDate DATETIME2 DEFAULT GETDATE(),

	CONSTRAINT FK_Bookings_Passenger 
	FOREIGN KEY (PassengerID) REFERENCES Passengers(PassengerID),

	CONSTRAINT FK_Bookings_Flight
	FOREIGN KEY (FlightID) REFERENCES Flights(FlightID),

	CONSTRAINT UQ_Booking_Seat
	UNIQUE (FlightID, SeatNumber),
);
ALTER TABLE Flights
ADD Price DECIMAL(10,2) NOT NULL DEFAULT 0.00;

ALTER TABLE Bookings
DROP COLUMN Price;

INSERT INTO Airports (AirportCode, AirportName, City, Country) VALUES
('LHR', 'Heathrow Airport', 'London', 'UK'),
('LGW', 'Gatwick Airport', 'London', 'UK'),
('JFK', 'John F. Kennedy International', 'New York', 'USA'),
('DXB', 'Dubai International', 'Dubai', 'UAE'),
('ISB', 'Islamabad International', 'Islamabad', 'Pakistan');

INSERT INTO Aircrafts (Model, Manufacturer, Capacity) VALUES
('A320', 'Airbus', 180),
('B737', 'Boeing', 160),
('A380', 'Airbus', 500),
('B777', 'Boeing', 300),
('E190', 'Embraer', 100);

Select * from Flights
Select * from Aircrafts
Select * from Airports
Select * from __EFMigrationsHistory
Select * from AspNetUsers
Select * from AspNetRoles
Select * from AspNetUserRoles
Select * from Passengers
Select * from Bookings

DELETE FROM __EFMigrationsHistory WHERE MigrationId = '20251109123815_InitialCreate';
