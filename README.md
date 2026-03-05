QR Warehouse Stock System
Overview

This is a web-based stock tracking system designed to manage products and warehouse operations. The system provides a secure login for users and utilizes JWT tokens for authentication. Depending on the credentials, users can have either Operator or Admin roles, with access to different features.

Features

Login: Users must log in to access the system. Authentication is performed using JWT tokens. Only valid usernames and passwords allow access to the homepage.

Header Options: After logging in, the header provides options for adding users, logging out, and operations, which reveal more features on hover.

Product Registration: Users can enter a product name and type. The system automatically records the registration date and stores the information in the database.

Product List: Displays all products in the system, including ID, name, type, and registration date.

Generate QR Code: Users can enter a text string, and the system generates the corresponding QR code on the screen.

Scan QR Code: Users can scan a QR code using their device camera. A 15-second timeout is applied:

If the scanned QR code does not match any product ID in the system, a "Product not found" message is displayed, along with a back button.

If the product ID exists, the system displays the product's name, type, and creation date.

Additionally, buttons appear below to add the product to the store or remove it from the store. Adding stores the product in the database, while removing updates the FromStoreExit field with the current date.

Warehouse Records: Displays stored products with details including Store ID, Product ID, Section, Entry Date, and Exit Date.

Add Section: Users can create new sections in the system, and all existing sections are displayed immediately below.

User Management

Add User: Allows the creation of a new user with a username and password.

Logout: Invalidates the JWT token stored on the client, effectively logging the user out.

Architecture

Frontend: ASP.NET Core MVC with Razor view engine. Uses Bootstrap for styling and standard CSS. Server-Side Rendering (SSR) is used, without additional JavaScript frameworks like React or Angular.

Backend: ASP.NET Core Web API with Controllers, Services, Data, Models, Migrations, and Program.cs. All database operations are performed via the API, which verifies JWT tokens for every request.

Database: Microsoft SQL Server, accessed through Entity Framework Core (EF Core).

QR Code: QR code generation is implemented using the QRCoder library, and camera scanning is integrated in the views with html5-qrcode.

Setup

Clone the repository.

Restore NuGet packages.

Configure appsettings.json with your SQL Server connection string.

Apply migrations using Update-Database.

Run the API project first (WebApplication5) and then the web client (WebApplication6).

Notes

All business logic, including product registration, store management, and section management, is implemented on the server side and exposed through the API.

The QR code scanner in the client view uses a 15-second timeout and validates that the scanned ID exists in the system.