# ArtGalleryAPI

### This is a .NET 8 Web API project for managing artworks in an art gallery domain. 
### This was a technical assignment request for hiring process.

The API allows for the management of artworks, including their details, stock quantities, and poster stock quantities. 
It follows best practices by implementing a service layer, using Entity Framework Core for database interactions, and adhering to SOLID principles and design patterns.
## Features

> - Product Management: Create, read, update, and delete artwork products.
> - Inventory Management: Manage stock quantities for both artworks and their posters.
> - Service Layer: Encapsulates business logic for better separation of concerns.
> - Entity Framework Core: Simplifies database interactions.
> - Concurrency Handling: Failed here. Initial implementation of byte[] timestamp RowVersion for optimistic concurrency control.
> - Validation and Exception Handling: Validates incoming data and handles exceptions gracefully.
> - SOLID Principles and Design Patterns: Ensures clean, maintainable, and testable code.
> - Unit Testing: Provides unit tests for business logic using XUnit and Moq.

## Prerequisites

  * .NET 8 SDK: Download and install from Microsoft .NET Download. https://dotnet.microsoft.com/pt-br/download
  * SQL Server: Required for the database (SQL Server Express ). https://www.microsoft.com/pt-br/sql-server/sql-server-downloads
  * Entity Framework Core CLI Tools


## Database ConnectionString
Change _*AppSettings.json*_ to your local SQLServer connectionString:

``
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ArtGalleryDB;Trusted_Connection=True;"
  },
}
``

## Future Enhancements

### Authentication and Authorization:

* Implement security measures to protect API endpoints using JWT or ASP.NET Core Identity.

### Pagination and Filtering:

* Enhance the GET /api/artworks endpoint to support pagination, sorting, and filtering.
