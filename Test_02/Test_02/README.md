# Racing Tournament API

## How to Run

1. Clone the repository
2. Navigate to the project directory
3. Update the connection string in `appsettings.json` to match your SQL Server configuration
4. Run the following commands to setup the database
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```
5. Run the application
    ```bash
    dotnet run
    ```
6. Use Swagger UI or Postman to test the API endpoints

## API Endpoints

- **GET /api/drivers**: Get all drivers. Sort by first name by default. You can sort by other parameters (e.g., last name, birthday).
- **GET /api/drivers/{id}**: Get driver by ID, including car and car manufacturer details.
- **GET /api/driver/competitions/{driverId}**: Get all competitions where a driver participates.
- **POST /api/drivers**: Create a new driver.
- **POST /api/driver/competitions**: Assign a driver to a competition.
