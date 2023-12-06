Overview of the Solution

 In this repository I have created a .NET MVC application. Below are the thing which I am doing in the application

 - Implement CRUD (Create, Read, Update, Delete) operations for the `Employees` table using **EF Core**
 - Another set of CRUD operations for the `Employees` table using **Dapper**.
 - Create Web API controller to expose the CRUD operations via HTTP methods (GET, POST, PUT, DELETE).


Technologies used
- .NET 6
- Entity Framework Core
- Dapper
- WebApI's

I have tried to follow Clean Architecture principles along with best practices such as creating Custom Exception class , meaningfull log messages etc. The project is divided into layers:
- Domain Layer
- Infrastrcuture Layer
- Business Logic Layer
- Presentation Layer
