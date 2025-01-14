# Water Company Web Application

A web application for managing water company services such as billing, meter requests, client management, and location services. The application supports authentication, role-based authorization, and data integrity measures such as preventing cascading deletes.

## Technologies Used

- **ASP.NET Core 5.0 MVC**: Framework for building web applications using the Model-View-Controller architecture.
- **Entity Framework Core**: ORM for interacting with the SQL Server database.
- **ASP.NET Core Identity**: Manages user authentication and role-based authorization.
- **JWT Authentication**: Secures API using JSON Web Tokens.
- **Syncfusion**: Provides advanced UI components like data grids and charts.
- **JavaScript**: Used for creating and styling the frontend of the application.
- **jQuery**: For DOM manipulation and AJAX requests.
- **Bootstrap**: For responsive design and UI components.
- **Google Maps API**: Integrated to show the company’s physical location.

## Key Features

### 1. **User Authentication & Role-based Authorization**
Users can register and log in using JWT authentication. The application supports role-based authorization with the following roles:
- **Admin**: Can manage users, clients, bills, and meter requests.
- **Employee**: Manages bills and processes meter requests.
- **Client**: Can view their own bills and request a meter.

### 2. **Client Management**
- **Admins** can view, add, edit, and delete client records.
- **Clients** can only view their own records.

### 3. **Bill Management**
- **Admins** and **Employees** can manage bills.
- **Clients** can only view their own bills, and deleting bills is restricted to prevent accidental data loss.

### 4. **Meter Requests**
- **Clients** can request a meter installation or maintenance.
- **Employees** process requests, and **Admins** can manage all requests.

### 5. **Google Maps Integration**
The app integrates with Google Maps to display the company’s physical location.

## Database Design

The application uses the following database entities:

- **Users**: Stores user data and role information.
- **Clients**: Contains client information.
- **Bills**: Stores billing data.
- **Countries**: Contains country data.
- **Cities**: Contains city data for the countries.
- **MeterRequests**: Stores water meter installation or maintenance requests.

## Setup

### 1. **Clone the Repository**

To run the project locally, follow the steps below:

1. **Clone the repository** to your local machine:

   ```bash
   git clone https://github.com/hugosousa42/WaterCompany.git
   ```

2.  **Navigate to the project folder:

   ```bash
   cd WaterCompany
   ```

### 2. **Minimum Requirements**

Before running the project locally, ensure you have the following prerequisites installed:

- **.NET SDK 5.0 or higher**: The project was developed using ASP.NET Core 5.0, so you need to have the .NET SDK installed.
  
  To install .NET, follow the official documentation: [Install .NET SDK](https://dotnet.microsoft.com/download).

- **SQL Server or SQL Server LocalDB**: The project uses SQL Server for database management. You can use **SQL Server LocalDB** if you don't want to install a full SQL Server instance.

  To install SQL Server, follow the official documentation: [SQL Server Downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).

- **Package Management Tool**: The application uses **NuGet** for package management, so ensure NuGet is installed in your development environment. Visual Studio and Visual Studio Code come with built-in support for it.

 ### 3. **NuGet Packages**

The project uses the following NuGet packages:

- **Microsoft.EntityFrameworkCore.Tools**: Provides tools for working with Entity Framework Core.
- **Microsoft.EntityFrameworkCore.SqlServer**: Enables SQL Server database provider for Entity Framework Core.
- **Microsoft.AspNetCore.Authentication.JwtBearer**: Used to add JWT authentication support to ASP.NET Core applications.
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore**: Provides ASP.NET Core Identity functionality backed by Entity Framework Core.
- **Syncfusion.EJ2.MVC5**: Syncfusion's JavaScript UI components for ASP.NET MVC, used for advanced data grids, charts, and calendars.
- **MailKit**: A cross-platform mail client library for .NET that supports sending and receiving emails.

To install these NuGet packages, you can either use **Visual Studio** or run the following command in your terminal (for each package):

```bash
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Syncfusion.EJ2.MVC5
dotnet add package MailKit
```

## Contributing

We welcome contributions to the project! If you'd like to contribute, follow these steps:

1. Fork the repository.
2. Create a new branch:
3. Make your changes.
4. Commit your changes:
5. Push to the branch:
6. Create a pull request.

If you find any bugs or issues, please report them by opening an issue in the GitHub repository.

## Contact Information

For questions or suggestions, feel free to reach out:

- **Email**: [hugosousa42@proton.me](mailto:hugosousa42@proton.me)
- **GitHub**: [hugosousa42](https://github.com/hugosousa42)
- **LinkedIn**: [Hugo Sousa](https://www.linkedin.com/in/hugo-sousa-dev/)




