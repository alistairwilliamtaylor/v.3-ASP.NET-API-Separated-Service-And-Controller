# v.3 ASP.NET Core REST API

## Introduction

This project marks the third step in my learning journey towards building a REST API with ASP.NET Core. I plan on refactoring the ShoppingItemsController by extracting the domain logic and database interactions out of the Controller and into a Services class.

Initially, [this explanation of the difference between controllers and services](https://www.coreycleary.me/what-is-the-difference-between-controllers-and-services-in-node-rest-apis) and [this follow up article explaining why it's a good idea to separate them](https://www.coreycleary.me/why-should-you-separate-controllers-from-services-in-node-rest-apis) were the clearest I found. Although the examples are all in Node.js, the concepts are explained in simple terms.

I also read this article about [splitting services and controllers as part of a clean onion architecture in .NET](https://code-maze.com/onion-architecture-in-aspnetcore/) but it seemed like far too much to take on at this stage, involved making 6 separate class libraries, etc.

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
