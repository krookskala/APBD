# Veterinary Clinic API

## Project Overview
This project develops a backend system for a veterinary clinic to manage animal records. It uses .NET technology and interacts with a SQL Server database using Entity Framework Core for CRUD operations.
## Key Features

- **Animal Management:** CRUD operations for animal records.
- **Search and Sort:** List animals with options to sort by name, description, category, or area.
- **Detailed Animal Retrieval:** Retrieve details about specific animals by ID.

## Prerequisites
Ensure you have the following installed:

- NET SDK (appropriate version, e.g., .NET 8)
- SQL Server (with access rights to execute SQL scripts)

## Setup Instructions

1. **Clone the Repository**
Clone the repository to your local machine using Git commands or by downloading the ZIP from the repository page.

2. **Configure Database Connection**
**Database Scripts:**
- Locate the SQL scripts in the scripts folder.
- Execute these scripts in your SQL Server to set up the database schema and initial data.

**Configuration Files:**
- Navigate to the src folder.
- Copy appsettings.Development.json to create a appsettings.json.
- Modify appsettings.json to include your database connection string.

3. **Build and Run the Application**
- Open the solution in Visual Studio or a similar IDE that supports .NET projects.
- Build the project to resolve dependencies.
- Run the application. The API will typically be hosted on localhost:5000.

## Using the API

- **Base URL:** Once the project is running, access the API at: 
```bash
http://localhost:5000/api/animals

```

- **Endpoints:**
-  GET /api/animals: List all animals. Add query string ?orderBy=<field> to sort by a specific field.
-  GET /api/animals/{id}: Retrieve a specific animal by ID.
-  POST /api/animals: Add a new animal.
-  PUT /api/animals/{id}: Update an existing animal or add it if it does not exist.

## Testing the API

Utilize the VeterinaryClinicApi.http file to send requests to the API. This file can be used with tools like JetBrains Rider or Visual Studio Code with the REST Client extension.