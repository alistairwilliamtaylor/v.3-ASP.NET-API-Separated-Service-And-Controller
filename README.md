# v.3 ASP.NET Core REST API

## Introduction

This project marks the third step in my learning journey towards building a REST API with ASP.NET Core. I plan on refactoring the ShoppingItemsController by extracting the domain logic and database interactions out of the Controller and into a Services class.

During this step, I might also look into the practice of using Data Transfer Objects (DTOs) to send data to the client in a suitable and convenient format, and mapping between these DTOs and the objects that are used by Entity Framework to create database tables.

## Learning for this milestone

### Creating A Services Class To Separate Concerns
Haven't done this yet

### Creating a DTO and Mappers
Haven't done this yet

## Next Steps
Not sure what they'll be at this stage.

## Installation
In order to run the program, you will need to have the .NET SDK installed on your computer.

You can install the .NET SDK using homebrew on the command line: `brew install --cask dotnet-sdk`

Alternatively, you can download the .NET SDK [here](https://dotnet.microsoft.com/en-us/)

To start up the API locally, use the command `dotnet run` from the CLI when inside the `FirstWebApp/` directory
