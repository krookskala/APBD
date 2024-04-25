# Warehouse API

## Overview
The Warehouse API is an ASP.NET Core application designed to manage a warehouse system, supporting operations such as adding and deleting products, and processing orders. It utilizes Entity Framework Core for ORM and direct SQL operations, featuring full CRUD capabilities and stored procedure integration.

## Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Visual Studio or VS Code

## Setup Instructions
1. **Clone the Repository**: `git clone https://github.com/krookskala/APBD/tree/main/LAB_07/WarehouseAPI && cd WarehouseAPI`
2. **Database Setup**:
    - Create `WarehouseDB` on SQL Server.
    - Run `scripts/create.sql` to set up the schema and initial data.
3. **Configuration**:
    - Modify `appsettings.json` to update the `Default` connection string.
4. **Run the Application**: Execute `dotnet run` within the project directory.
5. **Access Swagger UI**: Go to `http://localhost:5215/swagger` to interact with the API.

## API Endpoints
- **Add Product**: `POST /api/warehouse/add-product` - Accepts `InsertProductDto`.
- **Delete Product**: `DELETE /api/warehouse/delete-product/{productId}`.
- **Procedure Call**: `POST /api/warehouse/procedure` - Uses `InsertProductDto`.

## Development
To contribute:
1. Fork and clone the repo.
2. Create a feature branch: `git checkout -b feature/YourFeature`
3. Commit changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin feature/YourFeature`
5. Create a pull request.
