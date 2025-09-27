#  Student Enrollment System API

A RESTful API for managing courses, students, and enrollments, built with ASP.NET Core, EF Core, and Clean Architecture.

## Table of Contents
- [Architecture](#architecture)
- [Setup Instructions](#setup-instructions)
  - [Prerequisites](#prerequisites)
  - [Steps](#steps)
- [Dependencies](#dependencies)
- [Endpoints](#endpoints)
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

- BCrypt.Net-Next (Password hashing and verification)

- Microsoft.AspNetCore.Authentication.JwtBearer (JWT-based authentication)

## Endpoints
- POST /api/students/register: Register a new student.
- POST /api/students/login: Authenticate a student and return a JWT.
- GET /api/students/{id}: Get student by ID (requires authentication).
- GET /api/students: Get all students (requires authentication).
- PUT /api/students/{id}: Update a student (requires authentication).
- DELETE /api/students/{id}: Delete a student (requires authentication).
- POST /api/enrollments/enroll: Enroll a student in a course (requires authentication).
- GET /api/enrollments/student/{studentId}/courses: Get enrolled courses for a student (requires authentication).
- POST /api/courses/add-course: Create a course.
- PATCH /api/courses/update-course: Update a course.
- DELETE /api/courses/delete-course: Delete a course.
- GET /api/courses/{id}: Get a course by ID.
- GET /api/courses/courses: Get courses with optional filtering and pagination.

## Testing the API
```bash
dotnet run --project src/CourseCatalogue

Access Swagger: Open https://localhost:<port>/swagger to test endpoints.
Test Flow:
- Register a student (POST /api/students/register).
- Login to get a JWT (POST /api/students/login).
- Use the JWT in the Authorization header (e.g., Bearer <token>) for protected endpoints like POST /api/enrollments/enroll or GET /api/students/{id}.
-Test filtering/pagination with GET /api/courses/courses?title=math&instructor=smith&page=1&pageSize=10.

You can test the API using the Swagger UI or tools like Postman or cURL. Below are example requests for each endpoint:
- **Register Student**:
  ```bash
  POST /api/students/register
  Content-Type: application/json
  {
    "name": "John Doe",
    "email": "john@example.com",
    "dateOfBirth": "2000-01-01",
    "password": "Password123"
  }

- **Login**:
  ```bash
  POST /api/students/login
  Content-Type: application/json
  {
    "email": "john@example.com",
    "password": "Password123"
  }

- Enroll in Course:
  ```bash
  POST /api/enrollments/enroll
  Authorization: Bearer <token>
  Content-Type: application/json
  {
    "studentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "courseId": "3fa85f64-5717-4562-b3fc-2c963f66afa7"
  }

- Get Courses with Filtering and Pagination:
  ```bash
  GET /api/courses/courses?title=math&instructor=smith&page=1&pageSize=10
