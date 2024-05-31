# Veterinary Clinic API

## Overview

This project implements a RESTful API for managing a veterinary clinic, including endpoints for animals and visits.

## Endpoints

### Animals

- **GET /api/animals**: Get all animals, optionally sorted by "Name" or "Description".
- **GET /api/animals/{id}**: Get a specific animal by ID.
- **POST /api/animals**: Create a new animal.
- **PUT /api/animals/{id}**: Update an existing animal by ID.
- **DELETE /api/animals/{id}**: Delete an existing animal by ID.

### Visits

- **GET /api/visits**: Get all visits, ordered by date.
- **GET /api/visits/{id}**: Get a specific visit by ID.
- **POST /api/visits**: Create a new visit.
- **PUT /api/visits/{id}**: Update an existing visit by ID.
- **DELETE /api/visits/{id}**: Delete an existing visit by ID.

## Dependencies

- .NET 8.0
- Entity Framework Core
- SQL Server

## How to Execute

1. Clone the repository.
2. Navigate to the project directory.
3. Ensure SQL Server is running and update the connection string in `appsettings.json`.
4. Run `dotnet ef database update` to apply migrations.
5. Build and run the project using `dotnet run`.

## Logging

(Optional) Implement logging as needed for debugging and monitoring purposes.

