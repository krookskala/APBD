# AuthAPI

AuthAPI is an ASP.NET Core Web API designed to provide robust authentication and authorization functionalities. It includes features for user registration, login, and token management, utilizing JWT (JSON Web Tokens) for secure access control.

## Features

- **User Registration**: Enables new users to register by providing a username and password.
- **User Login**: Authenticates users and issues access and refresh tokens.
- **Token Refresh**: Allows users to renew their access tokens using refresh tokens.
- **Secure Token Handling**: Utilizes JWT for secure token generation and validation.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or another compatible IDE that supports .NET 8.0
- SQL Server (LocalDB or full version)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/krookskala/AuthAPI.git
   cd AuthAPI

2. **Restore NuGet packages**
   ```bash
   dotnet restore

3. **Set up the database**
- Ensure the connection string in `appsettings.json` is correct for your SQL Server instance.
- Run the following command to update the database using EF Core migrations:
   ```bash
  dotnet ef database update

4. **Run the application**
- From Visual Studio:
   - Open the solution file, set AuthAPI as the startup project, and hit run.
- From the command line:
   ```bash
   dotnet run


### Usage

- **Register a new user**: Send a POST request to `/api/auth/register` with a username and password.
- **Login**: Send a POST request to `/api/auth/login` with a username and password.
- **Refresh Token**: Send a POST request to `/api/auth/refresh` with a refresh token.

### API Endpoints

| Method | Endpoint             | Description            | Requires Authentication |
|--------|----------------------|------------------------|------------------------|
| POST   | `/api/auth/register` | Register a new user.   | No                     |
| POST   | `/api/auth/login`    | Authenticate a user.   | No                     |
| POST   | `/api/auth/refresh`  | Refresh access token.  | Yes                    |

### Built With

- [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) - The web framework used
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - ORM for database access
- [ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity) - For authentication and user management
