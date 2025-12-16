# WarehouseWebApp
Inventory Management System
A 3-layer ASP.NET Core MVC application for managing warehouse stock and employee access.

Architecture Overview
The project is divided into three distinct layers to ensure separation of concerns:

Presentation Layer (MVC): Handles UI, Controllers, and Cookie-based Authentication.

Business Layer: Processes logic, validates data, and bridges the UI and Database.

Data Layer: Manages raw SQL database communication and data models.

Core Features
🔐 Authentication & Authorization
Cookie Authentication: Implemented in Program.cs and AuthController.cs.

Role-Based Access Control (RBAC): Restricts "Guest" users from performing Create, Edit, or Delete operations.

Claims-Based Identity: Stores full names and roles within the authentication cookie for personalized UI.

📦 Inventory Management
CRUD Operations: Full Create, Read, Update, and Delete functionality for warehouse items.

Model Validation: Server-side validation using Data Annotations (e.g., [Required], [Range]) to ensure data integrity.

Real-time Search: Partial view implementation allowing instant inventory filtering via AJAX.

Project Structure & File Map
DataLayer
Models/: Contains Item.cs and Employee.cs (POCO classes).

Repositories/: Contains ItemRepository.cs and EmployeeRepository.cs using ADO.NET and SQL.

IItemRepository.cs: Interface defining database contracts.

BusinessLayer
ItemBusiness.cs: Handles item logic, including the search query filter and result conversion.

EmployeeBusiness.cs: Manages employee validation and password persistence logic.

PresentationLayer
Controllers/: Logic for Inventory, Employee, and Auth.

Views/: Razor pages including _InventoryTable.cshtml (Partial View) and Edit.cshtml with JavaScript features.

Program.cs: Configures Dependency Injection (DI) and Middleware.

Technical Implementation Details
Dependency Injection: Services are registered as Scoped in Program.cs using interfaces to decouple layers.

Frontend Logic: JavaScript used in Edit views for dynamic password toggles and Index views for AJAX search.

Security: Password fields in edit views are intentionally cleared to prevent accidental exposure or overwriting.
