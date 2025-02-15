# ASP.NET Core Web API Starter

**ASP.NET Core Web API Starter** is a foundational template for building modern REST APIs with **ASP.NET Core**. This project provides a pre-configured setup with essential dependencies, enabling developers to efficiently kickstart API development. It includes authentication, validation, and database connectivity, making it ideal for creating secure and scalable APIs.

## Dependencies
The following dependencies are used in this project:

* AutoMapper (14.0.0)
* BCrypt.Net-Next (4.0.3)
* Microsoft.AspNetCore.Authentication.JwtBearer (8.0)
* Microsoft.EntityFrameworkCore.SqlServer (8.0.13)
* Microsoft.EntityFrameworkCore.Tools (8.0.13)

## Configuration

To ensure the application runs correctly, you need to modify the following values in appsettings.json:

* Database: Replace `<DB_NAME>` and `<DB_PASSWORD>` in the connection string:
  ```js
  "ConnectionStrings": {
    "DbConnection": "Server=localhost; Database=<DB_NAME>; User ID=sa; Password=<DB_PASSWORD>; TrustServerCertificate=True;"
  }
  ```

* JWT: Replace `your_jwt_secret` with a secure key for signing JWT tokens:

  ```js
  "Jwt": {
    "Key": "your_jwt_secret"
  }
  ```

## Database Migration

To apply the initial database migration, execute the following commands:

```shell
Add-Migration Initial
Update-Database
```
