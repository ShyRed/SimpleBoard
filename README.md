# SimpleBoard
SimpleBoard is a very simple message board demo app written using
* [Angular 7](https://angular.io/)
* [Bootstrap 4](https://getbootstrap.com/)
* [.NET Core](https://dotnet.github.io/)
* [Nhibernate](https://nhibernate.info/)
* [SQLite](https://www.sqlite.org/index.html)

The purpose of this app is to experiment with said technologies. It is by no means meant to show best practices or even be deployed to production.

## Features
* User registration
* [JSON Web Token Security](https://jwt.io)
* Posting short messages to the board
* Reading messages with server side paging
* Model with fluent mapping
* Reading / Writing to database using NHibernate
* Automatic database file creation on first startup

## How to run and develop
The WebApi is written and build using Visual Studio 2017. Just open the solution, restore NuGet packages and run the .Net Core console project. The application run in a command prompt hosting the API using .Net Core's Kestrel server.
You can access the api documentation via http://localhost:5000/swagger

The angular frontend is developed using Visual Studio Code. Just open the SimpleBoard subdirectory in VS Code and use a terminal to execute `ng serve -o`. The frontend expects the WebApi to be running at localhost on port 5000.
