# ASP.NET Core Web API Starter

**ASP.NET Core Web API Starter** is a foundational template for building modern REST APIs with **ASP.NET Core**. This project provides a pre-configured setup with essential dependencies, enabling developers to efficiently kickstart API development. It includes authentication, validation, and database connectivity, making it ideal for creating secure and scalable APIs.

## Configuration

To ensure the application runs correctly, you need to modify the following values in appsettings.json:

* Database: Replace <DB_NAME> and <DB_PASSWORD> in the connection string:
  ```js
  "ConnectionStrings": {
    "DbConnection": "Server=localhost; Database=<DB_NAME>; User ID=sa; Password=<DB_PASSWORD>; TrustServerCertificate=True;"
  }
  ```

* JWT: Replace "your_jwt_secret" with a secure key for signing JWT tokens:

  ```js
  "Jwt": {
    "Key": "your_jwt_secret"
  }
  ```
