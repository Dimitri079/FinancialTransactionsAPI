# Financial Transactions API

## Project Description

This is a RESTful API for managing financial transactions, built with **ASP.NET Core** and **Entity Framework Core**. It supports creating, reading, updating, and deleting (CRUD) financial transactions, as well as generating summaries based on transaction data. This API is ideal for tracking transaction history, calculating balances, and generating financial insights.

## Features

- **CRUD Operations**: 
  - Create, read, update, and delete financial transactions.

## Tech Stack

- **ASP.NET Core** for building the API
- **Entity Framework Core** for database operations
- **Swagger** for API documentation

## Endpoints

- `POST /api/transactions` - Create a new transaction
- `GET /api/transactions` - Retrieve all transactions with optional filters
- `GET /api/transactions/{id}` - Retrieve a specific transaction by ID
- `PUT /api/transactions/{id}` - Update a specific transaction by ID
- `DELETE /api/transactions/{id}` - Delete a specific transaction by ID
- `GET /api/transactions/summary` - Get summary data (total credits, debits, net balance)

## Setup Instructions

1. **Clone the repository**.
2. **Install dependencies** using `.NET CLI`:
   ```
   dotnet restore
   dotnet ef database update
   dotnet run
