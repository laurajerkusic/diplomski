PhonebookProject
----------------

This is a diploma thesis project developed as part of a Computer Science degree.
The application enables contact and message management using a layered architecture, microservices, and an Angular frontend.


Project Structure
----------------

-Phonebook.API – main .NET Web API application (three-layer architecture: API, BLL, DAL)

-IdentityMicroservice – microservice for user authentication and authorization (JWT)

-MessageMicroservice – microservice handling messages and notifications

-PhonebookClient – Angular frontend application

Technologies Used
------------------

-ASP.NET Core 8.0

-Entity Framework Core

-Angular 17

-SQL Server Express

-JWT authentication

-REST API

How to Run the Project
-----------------------
1. Database Setup and Migrations
Each backend project uses Entity Framework Core migrations to set up the database schema.

Run migrations and update the database for each project:

Phonebook.API

```
cd Phonebook.API
dotnet ef database update
IdentityMicroservice

cd IdentityMicroservice
dotnet ef database update
MessageMicroservice

cd MessageMicroservice
dotnet ef database update
```

Note:
If migrations are not yet created, generate them first using:

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

2. Running Backend Services
Run backend services via Visual Studio or from the command line.

If needed, build each backend project before running:
```
cd Phonebook.API
dotnet build

cd ../IdentityMicroservice
dotnet build

cd ../MessageMicroservice
dotnet build
```

In Visual Studio
Open the solution PhonebookProject.sln

Set multiple startup projects:

Phonebook.API

IdentityMicroservice

MessageMicroservice

Run all (F5 or Debug → Start Debugging)

From the command line
Run each service individually:

```
cd path/to/service-folder
dotnet run
```
Typical ports (verify in each launchSettings.json):

Phonebook.API: http://localhost:5217

IdentityMicroservice: http://localhost:5810

MessageMicroservice: http://localhost:5805

3. Running the Angular Frontend
In terminal, open the PhonebookClient folder and run:

```
npm install
ng serve --proxy-config proxy.conf.json
Open your browser at:
```

http://localhost:4200


4. Important: First Admin User Setup
The first admin user must be added manually using the API through Swagger.
To do this:

Run the IdentityMicroservice backend.

Open Swagger UI, usually at:

http://localhost:5810/swagger/index.html
Use the POST /api/Auth/register endpoint to create the first admin user.

In the request body, provide JSON like this:

```
{
  "username": "admin",
  "password": "admin123",
  "role": "Admin"
}
```

Execute the request. If successful, the admin user will be created with the password admin123.

Login Credentials for Testing

Username: admin  
Password: admin123

Final Notes
-----------------
Each microservice may use the same or separate databases according to the connection strings in appsettings.json.

Make sure to run migrations and update the database before running each service.



