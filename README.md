# Hair Salon

#### Epicodus C# independent project, 07.13.18

#### By Abel Trotter

## Description

A .NET web app that allows the owner of a salon to add stylists and the stylist's respective clients.

## User Stories

* As a salon employee, I need to be able to see a list of all our stylists.
* As an employee, I need to be able to select a stylist, see their details, and see a list of all clients that belong to that stylist.
* As an employee, I need to add new stylists to our system when they are hired.
* As an employee, I need to be able to add new clients to a specific stylist. I should not be able to add a client if no stylists have been added.

## Setup on OSX

* Download and install .Net Core 1.1.4
* Download and install Mono
* Download and install MAMP 4.5
* Clone the repo
* Open MAMP and start the Apache and MySql servers
* Navigate to MAMP > Tools > phpMyAdmin and import the `abel_trotter.sql` file to create the database
* Navigate to MAMP > Tools > phpMyAdmin and import the `abel_trotter_test.sql` file to create the test database
* Run `dotnet restore` from project directory and test directory to install packages
* Run `dotnet build` from project directory and fix any build errors
* Run `dotnet test` from the test directory to run the testing suite
* Run `dotnet watch run` to start the server with the dotnet watch tool

## Contribution Requirements

1. Clone the repo
1. Make a new branch
1. Commit and push your changes
1. Create a PR

## Technologies Used

* .Net Core 1.1.4
* MAMP 4.5
* MySql
* Bootstrap 3.3.7
* jQuery 3.3.1
* jQuery Validate 1.6.0
* jQuery Validation Unobtrusive 3.2.6

## Links

* [Github Repo] (https://github.com/atrotter0/hair-salon)
* [Heroku App] (https://three-sixty-studios.herokuapp.com/) (Using Postgres and Npgsql package)

## License

This software is licensed under the MIT license.

Copyright (c) 2018 **Abel Trotter**
