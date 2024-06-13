﻿# Veterinary Clinic API

## Overview

This project implements a RESTful API for managing a veterinary clinic, including endpoints for animals and visits.

## Endpoints

### User 
- **Register**: `POST /api/auth/register`
- **Login**: `POST /api/auth/login`
- **Refresh Token**: `POST /api/auth/refresh`

### Animals

- **Get All Animals**: `GET /api/animals`
- **Get Animal By ID**: `GET /api/animals/{id}`
- **Add Animal**: `POST /api/animals`
- **Update Animal**: `PUT /api/animals/{id}`
- **Delete Animal**: `DELETE /api/animals/{id}` (Admin only)

### Visits

- **Get All Visits**: `GET /api/visits`
- **Get Visit By ID**: `GET /api/visits/{id}`
- **Add Visit**: `POST /api/visits`
- **Update Visit**: `PUT /api/visits/{id}`
- **Delete Visit**: `DELETE /api/visits/{id}` (Admin only)


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

