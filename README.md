# Course Catalogue API

A RESTful API for managing a course catalogue, built with ASP.NET Core and Entity Framework Core. The API supports CRUD operations for courses, with a SQL Server database for persistence.

## Table of Contents
- [Architecture](#architecture)
- [Setup Instructions](#setup-instructions
  - [Prerequisites](#prerequisites)
  - [Steps](#steps)
- [Dependencies](#dependencies)
- [Setup](#setup)
- [Testing the API](#testing-the-api)
  
## Architecture

The project follows a **Clean Architecture** approach with the following layers:

- **CourseCatalogue.API**: Contains the ASP.NET Core Web API, including controllers (`CoursesController`) and configuration (`Program.cs`, `appsettings.json`).
- **CourseCatalogue.Application**: Handles business logic, including services (`CourseService`) and DTOs (`CourseDto`, `CreateCourseDto`, `UpdateCourseDto`).
- **CourseCatalogue.Domain**: Defines core entities (`Course`) and response models (`ResponseModel`).
- **CourseCatalogue.Infrastructure**: Manages data persistence using Entity Framework Core (`CourseCatalogueContext`) and migrations.

The API uses a generic `ResponseModel` to standardize responses, including success/failure status, messages, and data.

## Setup Instructions

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or another SQL Server instance
- [Git](https://git-scm.com/downloads)

### Steps
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/<your-username>/CourseCatalog.git
   cd CourseCatalogue

   cd CourseCatalog

2. **Restore Dependencies**:
   ```bash
   dotnet restore

3. **Configure Database**:
   Update the connection string in src/CourseCatalog.API/appsettings.json to point to your SQL Server instance.

   Apply migrations to create the database:bash
   dotnet ef database update --project src/CourseCatalog.Infrastructure --startup-project src/CourseCatalog.API

5. **Run the Application**:
   ```bash
   dotnet run --project src/CourseCatalog.API

The API will be available at http://localhost:5000 (or the port specified in appsettings.json).

5. **Access Swagger UI**:
   Navigate to http://localhost:5000/swagger in your browser to view the API documentation and test endpoints.

##Dependencies
Microsoft.AspNetCore.Mvc (ASP.NET Core MVC)
Microsoft.EntityFrameworkCore.SqlServer (EF Core SQL Server provider)
Swashbuckle.AspNetCore (Swagger/OpenAPI integration)

##Testing the API
You can test the API using the Swagger UI or tools like Postman or cURL. Below are example requests for each endpoint:

1. Create a Coursebash

curl -X POST "http://localhost:5000/api/Courses/add-course" \
-H "Content-Type: application/json" \
-d '{
  "title": "Introduction to C#",
  "description": "Learn the basics of C# programming.",
  "duration": "10 hours",
  "instructor": "John Doe"
}'

Response (200 OK):json

{
  "isSuccessful": true,
  "message": "Course created successfully",
  "errors": [],
  "statusCode": 200,
  "data": "guid-value"
}

2. Update a Coursebash

curl -X PATCH "http://localhost:5000/api/Courses/update-course" \
-H "Content-Type: application/json" \
-d '{
  "id": "guid-value",
  "title": "Advanced C#",
  "description": "Deep dive into C# programming.",
  "duration": "15 hours",
  "instructor": "Jane Doe"
}'

Response (200 OK):json

{
  "isSuccessful": true,
  "message": "Course updated successfully",
  "errors": [],
  "statusCode": 200,
  "data": true
}

3. Delete a Coursebash

curl -X DELETE "http://localhost:5000/api/Courses/delete-course?id=guid-value"

Response (200 OK):json

{
  "isSuccessful": true,
  "message": "Course deleted successfully",
  "errors": [],
  "statusCode": 200,
  "data": true
}

4. Get a Course by IDbash

curl -X GET "http://localhost:5000/api/Courses/guid-value"

Response (200 OK):json

{
  "isSuccessful": true,
  "message": "Course retrieved successfully",
  "errors": [],
  "statusCode": 200,
  "data": {
    "id": "guid-value",
    "title": "Introduction to C#",
    "description": "Learn the basics of C# programming.",
    "duration": "10 hours",
    "instructor": "John Doe"
  }
}

5. Get All Coursesbash

curl -X GET "http://localhost:5000/api/Courses/courses"

Response (200 OK):json

{
  "isSuccessful": true,
  "message": "Courses retrieved successfully",
  "errors": [],
  "statusCode": 200,
  "data": [
    {
      "id": "guid-value",
      "title": "Introduction to C#",
      "description": "Learn the basics of C# programming.",
      "duration": "10 hours",
      "instructor": "John Doe"
    }
  ]
}

