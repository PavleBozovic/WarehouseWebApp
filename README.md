# WarehouseWebApp
# Inventory Management System

A 3-layer ASP.NET Core MVC application for managing warehouse stock and employee access.

## Architecture

The project is organized into three layers to ensure separation of concerns:

* **Presentation Layer (MVC):** Manages the UI, Controllers, and Middleware configuration.
* **Business Layer:** Handles logic, data validation, and bridges the UI and Data layers.
* **Data Layer:** Handles raw SQL communication using ADO.NET and defines Data Models.



---

## Technical Stack

* **Backend:** ASP.NET Core
* **Database:** SQL Server (ADO.NET / Microsoft.Data.SqlClient)
* **Authentication:** Cookie-based Authentication with Role-Based Access Control (RBAC)
* **Frontend:** Razor Pages, Bootstrap, JavaScript (AJAX/jQuery)

---

## File Map

### DataLayer
* **Models/**: `Item.cs`, `Employee.cs` (Contains Data Annotations for validation).
* **Repositories/**: `ItemRepository.cs`, `EmployeeRepository.cs` (SQL logic).
* **Interfaces**: `IItemRepository.cs`, `IEmployeeRepository.cs`.

### BusinessLayer
* **ItemBusiness.cs**: Contains CRUD logic and search filtering.
* **EmployeeBusiness.cs**: Handles employee validation and password security logic.

### PresentationLayer
* **Controllers/**: `InventoryController.cs`, `AuthController.cs`, `EmployeeController.cs`.
* **Views/**: Razor views including `_InventoryTable.cshtml` (Partial View for AJAX updates).
* **Program.cs**: Configures Dependency Injection (DI) and Authentication Middleware.



---

## Database Schema

Run the following SQL script to set up the necessary tables for this application:

```sql
-- Create Inventory Table
CREATE TABLE [dbo].[Inventory] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (100)  NOT NULL,
    [Manufacturer] NVARCHAR (100)  NOT NULL,
    [Quantity]     INT             NOT NULL,
    [Price]        DECIMAL (18, 2) NOT NULL,
    [Category]     NVARCHAR (50)   NOT NULL,
    [Description]  NVARCHAR (MAX)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Create Employee Table
CREATE TABLE [dbo].[Employees] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50)  NOT NULL,
    [Surname]  NVARCHAR (50)  NOT NULL,
    [Username] NVARCHAR (50)  NOT NULL UNIQUE,
    [Password] NVARCHAR (255) NOT NULL,
    [Role]     NVARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
