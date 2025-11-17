# ***✈️ FLIGHT MANAGEMENT SYSTEM***

A **.NET Core 8 MVC** project with **SQL Server** database and **IIS Out-Of-Process hosting**.

---

## **📌 Overview**

The Flight Management System is an **ASP.NET Core 8 MVC web application** that manages:

- Flight information  
- Bookings  
- Admin operations  

It uses:

- **ASP.NET Core 8 MVC**  
- **Entity Framework Core**  
- **SQL Server**  
- **IIS (Out-of-Process Hosting)**  
- **Clean folder structure** (Web project, Data project, Database folder)

---

## **🚀 How to Run Locally**

### **1️⃣ Clone the Repository**

```bash
git clone https://github.com/returnvoid404/FlightManagementSystem.git
cd FlightManagementSystem
```

---

## **🗄️ Database Setup**

You can create the database in two ways:

---

### **✅ Option A — Run SQL Script (SSMS)**

1. Open **SQL Server Management Studio**  
2. Connect to your SQL Server  
3. Click **New Query**  
4. Load `Database/init.sql`  
5. Click **Execute**

**Using `sqlcmd`:**

```bash
sqlcmd -S YOUR_SERVER -U USER -P PASSWORD -i Database/init.sql
```

---

### **✅ Option B — Use EF Core Migrations**

```bash
dotnet ef database update --project FlightData --startup-project FlightWebApp
```

---

## **⚙️ Configuration**

### **📌 appsettings.json Connection String**

**SQL Authentication:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SERVER_NAME;Database=FlightManagementSystem;User Id=DB_USER;Password=DB_PASSWORD;"
  }
}
```

**Windows Authentication:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SERVER_NAME;Database=FlightManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

➡️ **Never commit real passwords.**

Use **User Secrets** during development:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" ""
```

---

## **▶️ Run the Application**

```bash
cd FlightWebApp
dotnet run
```

Then open:

👉 https://localhost:5001

---


## **🐞 Troubleshooting**

### **❌ 502.5 ANCM Error**

Possible causes:

- Missing **.NET Hosting Bundle**
- Wrong **runtime version**
- File permission issues
- Exceptions during startup

Check **Event Viewer** for full details.

---

### **❌ Database Connection Issues**

- Incorrect connection string  
- SQL authentication disabled  
- Firewall blocking access  
- IIS application pool identity missing DB permissions  

---

## **🤝 Contributing**

Pull requests and issue reports are welcome!

---

This project is open-source under the **MIT License**.
