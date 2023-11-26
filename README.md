# Eclectic Forums

_Web Based Online Forums Application_

An Online Forum for discussing my many (and vaired) interests. I named ths project "Eclectic Forums" because when I was in the Navy, I was discussing my many (and varied) interests in my shop one day. That is when my Chief looked at me and said "Murphy, you must be the most eclectic m'fer I ever met in my life". 

View Eclectic Forums Application Live [Link to Application](https://forumproject.azurewebsites.net/) 


## Instructions

To Download and Run With a Local PostgreSQL Database

I. Prepare Database for Entity Framework Core Migration

1. Download [PostgreSQL](https://www.postgresql.org/download/)/[PGAdmin](https://www.pgadmin.org/) 
2. Create a database in PostgreSQL.

II. Install Files to Run Locally

1. Clone code locally from [GitHub](https://github.com/danielmurphy1/ForumProject) and Open in [Visual Studio](https://visualstudio.microsoft.com/).
2. Open `launchSettings.json` from the root project directory and change lines 16 and 24 from `"Production"` to `"Development"`.
3. Open `appsettings.Development.json` from the root project directory and replace the value of line 10 with your database connection string.
4. Open the `Package Manager` window in Visual Studio and run `Update-Database` to create the tables in your database (alternatively, you can run `dotnet ef update database` from a terminal that is navigated to the root of the project.)
5. CD into `ClientApp` directory.
6. Run `npm i` in `ClientApp` directory in the terminal.
7. Open `forum-data.service.ts` in `ClientApp/src/app/services` - comment out line 18 and uncomment line 16.
8. Open `auth.service.ts` in `ClientApp/src/app/services` - comment out line 12 and uncomment line 10.
9. Press the start button at the top of the Visual Studio window. If your browser does not open automatically, open a browser and enter `https://localhost:44403/` to run the application.

### Application Screenshots
![Full Screen-Boards](https://github.com/danielmurphy1/ForumProject/blob/main/Screenshots/Boards-Full.JPG)

![Mobile/Responsive Screen-Boards](https://github.com/danielmurphy1/ForumProject/blob/main/Screenshots/Boards-Mobile.JPG)

![Full Screen-Posts](https://github.com/danielmurphy1/ForumProject/blob/main/Screenshots/Posts-Full.JPG)

![Mobile/Responsive Screen-Posts](https://github.com/danielmurphy1/ForumProject/blob/main/Screenshots/Posts-Mobile.JPG)

![Full Screen-Replies](https://github.com/danielmurphy1/ForumProject/blob/main/Screenshots/Replies-Full.JPG)

![Mobile/Responsive Screen-Replies](https://github.com/danielmurphy1/ForumProject/blob/main/Screenshots/Replies-Mobile.JPG)

## Technologies/Design

### Technology Stack

- TypeScript/JavaScript with Angular Framework, CSS with Boostrap Library - Front End Stack
- C# with ASP.NET Web Application (ASP.NET Core 6) - Backend Stack and Internal/REST API
- Entity Framework Core - ORM for .NET Core
- PostgreSQL - Database with Npgsql Nuget Package
- Microsoft Azure - Hosting platform


### Summary

This application is the third full-stack application that I have created since beginning to learn C#/.NET. It is also the first time that I have used an ORM, specifically in this case, Entity Framework Core. I learned a lot about EF core creating this project from setting up the entitnties for migration, to creating migrations, and updating the database using EF Core. I continued my previously learning of dependency injection from my previous C#/.NET projects and used it in this project in order to be more efficient in handling services. I was able to inject the service classes where needed so that they were available to be used without having to create new objects of each class each time. 

Additionally, this was the first time that I integrated authentication and authorization in a C#/.NET project. Previously I had done so in Node.js in personal and professional projects. I found the process in C#/.NET to be much more straight-forward and simpler, likely due to my experience doing so previously in Node.

Moreover, I learned about and how to use Data Transfer Objects (DTOs) to handle data being sent from the server to the client. Using the DTOs allowed me to pass data to the client without exposing full entities to the client and thus compromising ciritical data (such as user passwords). After creating this project my confidence in C#/.NET has increased extensibly, and I look forward to learning even more.

With this project, I also continued to learn more in the Angular Framework as well. I used Angular Guards for route protection for the first time and well as Interceptors as middleware to modify http requests being sent to the server. Additionally, I learned more about Subjects and BehaviorSubjects to track and use client state through a service so that components could share that state for authorization. Angular is such a robust framework, and each time that I create a project I learn more and more about it.

Overall, this project was a wonderfull exercise in learning new concepts and practicing those that I had already learned. 

### Author

- Dan Murphy, Full-Stack Developer, https://www.linkedin.com/in/daniel-murphy-055/, https://danielmurphy.dev