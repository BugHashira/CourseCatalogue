#  Student Enrollment System API

A RESTful API for managing courses, students, and enrollments, built with ASP.NET Core, EF Core, and Clean Architecture.

## Table of Contents
- [Architecture](#architecture)
- [Setup Instructions](#setup-instructions)
  - [Prerequisites](#prerequisites)
  - [Steps](#steps)
- [Dependencies](#dependencies)
- [Testing the API](#testing-the-api)
  
## Architecture

The project follows a **Clean Architecture** approach with the following layers:

- **Domain**: Entities (`Course`, `Student`, `Enrollment`) and `ResponseModel`.
- **Application**: DTOs, service interfaces, and business logic (services directly use `CourseCatalogueContext`).
- **Infrastructure**: EF Core DbContext.
- **Presentation**: Controllers, Swagger, and JWT configuration.

The API uses a generic `ResponseModel` to standardize responses, including success/failure status, messages, and data.

## Setup Instructions

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or another SQL Server instance
- [Git](https://git-scm.com/downloads)

### Steps
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/BugHashira/CourseCatalogue

2. **Install Dependencies**:
   ```bash
   dotnet restore

3. **Configure Database**:
   Update the connection string in src/CourseCatalog.API/appsettings.json to point to your SQL Server instance.

   Apply migrations to create the database:
   ```bash
   dotnet ef database update --project src/CourseCatalogue

5. **Run the Application**:
   ```bash
   dotnet run --project src/CourseCatalogue

The API will be available at http://localhost:7071 (or the port specified in appsettings.json).

5. **Access Swagger UI**:
   Navigate to http://localhost:7071/swagger in your browser to view the API documentation and test endpoints.

## Dependencies

- Microsoft.AspNetCore.Mvc (ASP.NET Core MVC)
  
- Microsoft.EntityFrameworkCore.SqlServer (EF Core SQL Server provider)
  
- Swashbuckle.AspNetCore (Swagger/OpenAPI integration)

## Testing the API

You can test the API using the Swagger UI or tools like Postman or cURL. Below are example requests for each endpoint:

1. Create a Course:

   ```bash
   curl -X POST "http://localhost:5000/api/Courses/add-course" \
-H "Content-Type: application/json" \
-d '{
  "title": "Introduction to C#",
  "description": "Learn the basics of C# programming.",
  "duration": "10 hours",
  "instructor": "John Doe"
}'

Response (200 OK):

json

{
  "isSuccessful": true,
  "message": "Course created successfully",
  "errors": [],
  "statusCode": 200,
  "data": "guid-value"
}

2. Update a Course
   
   ```bash
   curl -X PATCH "http://localhost:5000/api/Courses/update-course" \
-H "Content-Type: application/json" \
-d '{
  "id": "guid-value",
  "title": "Advanced C#",
  "description": "Deep dive into C# programming.",
  "duration": "15 hours",
  "instructor": "Jane Doe"
}'

Response (200 OK):

json

{
  "isSuccessful": true,
  "message": "Course updated successfully",
  "errors": [],
  "statusCode": 200,
  "data": true
}

3. Delete a Course:
4. 
   ```bash
   curl -X DELETE "http://localhost:5000/api/Courses/delete-course?id=guid-value"

Response (200 OK):

json

{
  "isSuccessful": true,
  "message": "Course deleted successfully",
  "errors": [],
  "statusCode": 200,
  "data": true
}

4. Get a Course by ID:

   ```bash
   curl -X GET "http://localhost:5000/api/Courses/guid-value"

Response (200 OK):

json

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

5. Get All Courses

   ```bash

   curl -X GET "http://localhost:5000/api/Courses/courses"

Response (200 OK):

json

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

