**RealtimeDbListener** is a .NET 8/9 minimal API project that listens to changes in a **SQL Server table** using `SqlDependency` and notifies connected clients via **SignalR** in real-time.

---

## 🚀 Features
- Real-time table change detection (INSERT / UPDATE / DELETE)
- SignalR Hub for broadcasting database changes
- Configurable SQL query via `appsettings.json`
- Works with Rider, Visual Studio, or VS Code
- Minimal, dependency-light architecture

---

## ⚙️ Setup

### 1️⃣ Database Setup (SQL Server)
Run these commands **once** in your database:

```sql
-- Enable Service Broker
ALTER DATABASE MyDb SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE;

-- Grant permissions to your SQL login (e.g., sa or app user)
GRANT SUBSCRIBE QUERY NOTIFICATIONS TO [YourUser];

-- Example test table
CREATE TABLE dbo.Customers (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- Insert sample data
INSERT INTO dbo.Customers (Name) VALUES ('Ahmet');
INSERT INTO dbo.Customers (Name) VALUES ('Mehmet')