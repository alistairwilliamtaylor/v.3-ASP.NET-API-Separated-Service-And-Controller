# First ASP.NET Core REST API

## Introduction

This project marks the first milestone in my learning journey towards building a REST API with ASP.NET Core. The API allows for shopping list items to be added, removed, updated, and viewed. 

At this point, I have learned the basics of writing GET, POST, DELETE, PUT, and PATCH endpoints, and writing requests using Postman to make use of these.

After watching [this "Learning REST APIs" linkedin learning series](https://www.linkedin.com/learning/learning-rest-apis/welcome) on the theory behind REST APIs and muddling through this very simple implementation, I feel like this has been a distinct and digestible chunk of learning, so I wanted to document my progress at this stage before moving forward.

## Learning so far

### Basic Routing and Controller Set-Up
Being completely new to ASP .NET Core there were a lot of little conventions to familiarise myself with. That `[Route("api/[controller]")]` would use whatever the prefix to "Controller" is in the name of the class to construct the URI, wasn't an immediately obvious convention to me.

Also, that the `[ApiController]` flag for the class means that the methods which need to use Body data from requests (such as POST and PUT) will automatically receive them passed through as a parameter, without even needing a `[FromBody]` flag took me a while to catch on to.

Finally, I needed to learn the syntax for adding URL parameters to routes which identified a single resource, and that this parameter will then be passed through to the method as an argument:
```
[HttpDelete("{id:int}")]
public ShoppingItem DeleteItem(int id)
{
    return _itemRepo.Remove(id);
}
```

### Adding a Repository to the Service Provider
I stumbled around trying to figure out how to make my simple Dictionary implementation of an ItemRepository persist between requests. 

In the end I was able to find that I could add the inject the repository into my application as a Singleton using the Service Provider with:

`builder.Services.AddSingleton<IItemRepository, InMemoryItemRepository>()`

and that would solve the problem and get me what I want. But it was only after Tom later explained to me roughly what's going on here and what the Dependency Injection Container pattern is trying to achieve that I had a loose idea of what was actually going on

### Implementing PATCH
It took me a bit of searching and reading to find out how to write a PATCH route, because I needed to install the [NewtonsoftJson Nuget package](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.NewtonsoftJson/)  and then I also needed to learn about what a PATCH request body looks like - I was only loosely familiar with GET, POST, and DELETE methods before, but apparently not PATCH at all!

## Next Steps

### Entity Framework
One of the next steps in the project will be to install and familiarise myself with Entity Framework so that I can begin to make use of it.

### Handling Bad Requests and Using Error Codes
Currently the methods contain almost no logic at all and always assume the happy path. The next step would be to 

## Installation
In order to run the program, you will need to have the .NET SDK installed on your computer.

You can install the .NET SDK using homebrew on the command line: `brew install --cask dotnet-sdk`

Alternatively, you can download the .NET SDK [here](https://dotnet.microsoft.com/en-us/)

To start up the API locally, use the command `dotnet run` from the CLI when inside the `FirstWebApp/` directory
