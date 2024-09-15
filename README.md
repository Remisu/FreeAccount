
# FreeAccount API

## Project Overview

FreeAccount API is a banking account simulation project designed to explore and practice new technologies, as well as to demonstrate optimal code organization and usage. The project simulates the creation and management of bank accounts, handling operations such as account creation, balance transfers, balance updates, and account deletion, all while implementing essential patterns such as command-query separation, validation, and error handling.

## Features

- **Account Creation**: Create bank accounts with initial balance, including basic validations for Tax Number and email.
- **Account Management**: Retrieve, update, or delete accounts based on Tax Number, with comprehensive validation.
- **Transaction Handling**: Add funds and transfer balances between accounts.
- **Validation**: Strong validation mechanisms are implemented using FluentValidation to ensure input accuracy.
- **Error Handling**: Custom middleware handles exceptions and provides detailed error messages.

## Project Structure

- **FreeAccount.Api**: 
  - Contains API controllers to handle HTTP requests for account operations.
  - Uses ASP.NET Core with Swagger for API documentation.
  
- **FreeAccount.Domain**:
  - Contains the core logic for command handling, query processing, and domain exceptions.
  - Implements the command and query patterns using MediatR for better separation of concerns.

- **FreeAccount.Ioc**:
  - Handles dependency injection and middleware configurations.
  - Adds custom validation and exception handling behaviors to MediatR.

## Key Files and Classes

- `AccountController.cs`: Manages endpoints for creating, retrieving, updating, and deleting bank accounts.
- `TransactionsController.cs`: Handles transactions like fund transfers and balance additions.
- `CreateAccountCommand.cs`: Represents the command for creating a bank account.
- `ValidationBehavior.cs`: Adds validation logic to commands, ensuring data consistency.
- `ExceptionHandlingMiddleware.cs`: Middleware that captures and handles all application exceptions, returning detailed error responses.

## Technologies Used

- **ASP.NET Core**: Used to build the API endpoints.
- **MediatR**: Implements command and query separation for better code organization.
- **FluentValidation**: Ensures proper validation of request data.
- **Serilog**: Handles logging throughout the application.
- **Swagger**: Provides API documentation and testing interface.

## Installation

1. Clone the repository.
   ```bash
   git clone <repository-url>
   ```

2. Navigate to the project folder and build the solution.
   ```bash
   cd FreeAccount.Api
   dotnet build
   ```

3. Run the application.
   ```bash
   dotnet run
   ```

4. Open your browser and navigate to `http://localhost:<port>/swagger` to access the Swagger UI for API testing.
