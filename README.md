

# 🏪 Store Management API

A RESTful Web API built with **ASP.NET Core Web API and C#** for managing products and categories, with secure authentication and role-based authorization.

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-purple)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/Language-C%23-239120)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Entity Framework Core](https://img.shields.io/badge/ORM-Entity%20Framework%20Core-blue)](https://learn.microsoft.com/en-us/ef/core/)
[![PostgreSQL](https://img.shields.io/badge/Database-PostgreSQL-4169E1)](https://www.postgresql.org/)
[![JWT](https://img.shields.io/badge/Auth-JWT-orange)](https://jwt.io/)
[![Render](https://img.shields.io/badge/Deployment-Render-46E3B7)](https://render.com/)

## 📌 About the Project

Store Management API is a backend project designed to organize and manage store products and categories through RESTful HTTP endpoints.

The project uses Entity Framework Core for database access, ASP.NET Core Identity for user management, and JWT for authentication and role-based authorization.

## 🚀 Live Demo

- **Swagger UI:** [Explore Store Management API](https://store-management-1-7wrw.onrender.com/swagger/index.html)
- **API Base URL:** https://store-management-1-7wrw.onrender.com


## ✨ Features

* **User Authentication:** Register and log in to the application.
* **JWT Authentication:** Authenticate API requests using bearer tokens.
* **Role-Based Authorization:** Protect endpoints based on user roles, including Admin.
* **Category Management:** Create, retrieve, update, and delete categories.
* **Product Management:** Create, retrieve, update, and delete products.
* **Product–Category Relationship:** Associate each product with an existing category.
* **Data Validation:** Validate requests and category references.
* **Swagger / OpenAPI:** Explore and test API endpoints.

## 🛠️ Technologies

* C#
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL (Neon)
* ASP.NET Core Identity
* JWT Bearer Authentication
* Swagger / OpenAPI
* Docker
* Render

## 📂 Project Structure

```text
Store-Management/
├── Auth/
├── Controllers/
├── DTO/
├── Data/
├── Migrations/
├── Models/
├── Properties/
├── Dockerfile
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
└── StoreManagment.csproj
```

## 🚀 Getting Started

### Prerequisites

* .NET 8 SDK
* Visual Studio or Visual Studio Code
* PostgreSQL database

### 1. Clone the Repository

```bash
git clone https://github.com/aqdarahmad/Store-Management.git
cd Store-Management
```

### 2. Configure the Database

Configure the PostgreSQL connection string using ASP.NET Core User Secrets.

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_POSTGRESQL_CONNECTION_STRING"
```

Replace the placeholder with your PostgreSQL connection string from Neon or another PostgreSQL provider.

### 3. Configure JWT

Store the JWT signing key securely using ASP.NET Core User Secrets during local development.

Initialize User Secrets if necessary:

```bash
dotnet user-secrets init
```

Set your secret key:

```bash
dotnet user-secrets set "Jwt:Key" "YOUR_STRONG_RANDOM_SECRET_KEY"
```

Make sure the application reads the key from configuration and uses the same key for token creation and validation.

**Security:** Never commit real signing keys, passwords, or access tokens to GitHub.

### 4. Run the Application

Open the solution in Visual Studio and run the project, or use:

```bash
dotnet restore
dotnet run
```

### 5. Open Swagger

When the application is running, open the Swagger URL printed in the terminal.

For the deployed application, use:

[https://store-management-1-7wrw.onrender.com/swagger/index.html](https://store-management-1-7wrw.onrender.com/swagger/index.html)

## 📡 API Endpoints

### Authentication

| Method | Endpoint | Description |
| ------ | ------ | ------ |
| POST | `/api/Auth/register` | Register a new user |
| POST | `/api/Auth/Login` | Authenticate and obtain a JWT |

### Categories

| Method | Endpoint | Description |
| ------ | ------ | ------ |
| GET | `/api/Category` | Get categories |
| GET | `/api/Category/{id}` | Get a category by ID |
| POST | `/api/Category` | Create a category |
| PUT | `/api/Category/{id}` | Update a category |
| DELETE | `/api/Category/{id}` | Delete a category |

### Products

| Method | Endpoint | Description |
| ------ | ------ | ------ |
| GET | `/api/Product` | Get products |
| GET | `/api/Product/{id}` | Get a product by ID |
| POST | `/api/Product` | Create a product |
| PUT | `/api/Product/{id}` | Update a product |
| DELETE | `/api/Product/{id}` | Delete a product |

## 🔐 Authentication Guide

1. Register an account using `/api/Auth/register`.
2. Log in using `/api/Auth/Login`.
3. Copy the JWT returned by the login endpoint.
4. Click **Authorize** in Swagger UI and enter the bearer token in the required format.
5. Use the protected endpoints according to your assigned role.

Example of an administrator-only endpoint:

```csharp
[Authorize(Roles = "Admin")]
```

The user must have the required role for role-based authorization to succeed.

## 🔒 Security

* Keep JWT signing keys outside source control.
* Use User Secrets for local development.
* Store production database credentials in Render environment variables.
* Use HTTPS when accessing the deployed API.
