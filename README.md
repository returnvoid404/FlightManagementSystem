***Flight Management System***
A .NET Core 8 MVC project with SQL Server
database and IIS Out-Of-Process hosting

------------------------------------------------------------------------

Overview The Flight Management System is an ASP.NET Core 8 MVC web
application that manages flight information, bookings, and
administrative operations.
It uses: - ASP.NET Core 8 MVC - Entity Framework Core - SQL Server -
Out-of-process hosting with IIS as a reverse proxy - A clean folder
structure (Web project, Data project, Database folder)

------------------------------------------------------------------------

How to Run Locally

1.  Clone the repository git clone
    https://github.com/returnvoid404/FlightManagementSystem.git cd
    FlightManagementSystem

------------------------------------------------------------------------

Database Setup

You can create the database in two ways:

Option A — Run the SQL script using SSMS 1. Open SQL Server Management
Studio 2. Connect to SQL Server 3. Open “New Query” 4. Load
Database/init.sql 5. Click Execute

Using sqlcmd: sqlcmd -S YOUR_SERVER -U USER -P PASSWORD -i
Database/init.sql

Option B — Use EF Core migrations dotnet ef database update –project
FlightData –startup-project FlightWebApp

------------------------------------------------------------------------

Configuration

Connection string template for appsettings.json:

{ “ConnectionStrings”: { “DefaultConnection”:
“Server=SERVER_NAME;Database=FlightManagementSystem;User
Id=DB_USER;Password=DB_PASSWORD;” } }

OR If you are using windows authentication then use this Connecting String
{ “ConnectionStrings”: { “DefaultConnection”:
“Server=SERVER_NAME;Database=FlightManagementSystem;
Trusted_Connection=True;TrustServerCertificate=True;” } } 

Never commit real passwords.

Use User Secrets in development: dotnet user-secrets init dotnet
user-secrets set “ConnectionStrings:DefaultConnection” “”

------------------------------------------------------------------------

Run the Application

cd FlightWebApp dotnet run

Then open: https://localhost:5001

------------------------------------------------------------------------

Deploying to IIS (Out-of-Process Hosting)

1.  Install Requirements

    -   .NET 8 Hosting Bundle
    -   IIS + Management Tools

2.  Publish the Project dotnet publish FlightWebApp -c Release -o
    ./publish

3.  Copy Published Folder to Server Example: C:

4.  Configure IIS

    -   Add Website → point to publish folder
    -   Application Pool: • No Managed Code • Integrated Pipeline
    -   Ensure web.config contains:

5.  Set production connection string using:

    -   Environment variables
    -   IIS Configuration Editor

6.  Restart IIS Website

------------------------------------------------------------------------

Troubleshooting

502.5 ANCM Error - Missing .NET Hosting Bundle - Wrong runtime - File
permission issues - Startup exceptions

Check Event Viewer for details.

Database connection issues - Incorrect connection string - SQL
authentication disabled - Firewall blocking access - IIS identity lacks
permissions

------------------------------------------------------------------------

Contributing Feel free to fork, open issues, or make pull requests.

------------------------------------------------------------------------

License This project is open-source under the MIT License.
