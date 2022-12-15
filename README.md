# Project

To run the project:

1. Run dotnet ef database update (if that doesn't work delete the migrations folder and add the migrations: dotnet ef migrations add InitialCreate and then run dotnet ef database update) from the root of the project
2. Run dotnet watch run and see the API with Swagger UI in the browser

Included nuget packages:
1. Automapper: https://www.nuget.org/packages/automapper/
2. Automapper dependency injection: https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection/
3. Microsoft EntityFrameworkCore Design: https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design
4. Microsoft EntityFrameworkCore Sqlite: https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/
5. Slugify: https://www.nuget.org/packages/Slugify/

App used: Microsoft Asp.net Core application
IDE used: Visual Studio Community Edition
