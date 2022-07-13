# Second ASP.NET Core REST API

## Introduction

This project marks the second step in my learning journey towards building a REST API with ASP.NET Core. At this point, Entity framework is set up with models for shopping lists and for shopping items, with a single list being able to contain multiple items. However, I have only written a controller and logic for shopping items, and will work on filling out the API functionality once I have refactored the project in the next stage.

To learn the content for this stage, I primarily followed along with [Part 2. "Retrieving Data" from the linkedin learning course "Building Web APIs with ASP.NET Core in .NET 6"](https://www.linkedin.com/learning/building-web-apis-with-asp-dot-net-core-in-dot-net-6/building-web-apis). This taught me how to set up Entity Framework for the first time and create a very simple, two resource, in-memory database. I then set about adapting the API to make use of this new DbContext, introduced some basic error handling and responses with different HTTP codes, and re-wrote the routes to be asynchronous using async/await.

## Learning for this milestone

### Setting up Entity Framework
This was a fascinating learning journey, as I had never used an ORM before. I learned about the following methods which can be used to declaratively set out the one->many relationships which exist within the database.
```
modelBuilder.Entity<ShoppingList>()
    .HasMany(list => list.Items)
    .WithOne(item => item.ShoppingList)
    .HasForeignKey(item => item.ShoppingListId);
```
Though I also have subsequently been told that this was probably superfluous, since there's some major giveaways about these relationships in the Models for ShoppingItem and ShoppingList (ShoppingList has a `List<ShoppingItem>` field, and ShoppingItem has a field for the `ShoppingList` which it belongs to). Apparently, Entity Framework is smart enough to figure these simple kind of relationships out for itself, so you can omit them in simple cases like this and simply declare the DbSets:

```
    public DbSet<ShoppingItem> Items { get; set; }
    public DbSet<ShoppingList> Lists { get; set; }
```

I also created some seed data using the `.HasData` method.

Finally, I just needed to inject the DbContext into my application using the Service Provider like this:
```
builder.Services.AddDbContext<ShoppingContext>(options =>
    {
        options.UseInMemoryDatabase("Shopping Lists");
    })
```

Luckily, since I already had experienced injecting an in-memory repository that I had built myself, I had a rough idea of what was going on here and what I was achieving by adding this line in my Program.cs file.

### Asynchronous ActionResults with Error Codes

I switched over to making the API calls asynchronous, which would make a big performance difference when working with an API that is receiving and processing a large number of calls. This involved using `async`/`await` syntax, changing the return value to a `Task<>` and using appropriate ASync varieties of Entity Framework calls (e.g. `FindAsync()` and `SaveChangesAsync()`)

I also learned to make appropriate HTTP Code Responses, depending on the outcome of the request. Since functions such as `Ok()` and `NotFound()` wrap the body of the response, the return value of the method changes to `ActionResult<>` rather than simply the type of the object returned (e.g. `ShoppingItem`)

```
[HttpGet("{id:int}")]
public async Task<ActionResult<ShoppingItem>> GetItem(int id)
{
    var product = await _context.Items.FindAsync(id);
    if (product == null)
    {
        return NotFound();
    }
    return Ok(product);
}
```

## Next Steps

The next step will be to refactor the ShoppingItemsController by extracting the domain logic and database interactions out of the Controller and into a Services class. That way the Controller will remain simple and uncluttered, and won't become a "Fat Controller".

During this next step, I might also look into the practice of using Data Transfer Objects (DTOs) to send data to the client in a suitable and convenient format, and mapping between these DTOs and the objects that used in the domain logic and by Entity Framework to create database tables. 

## Installation
In order to run the program, you will need to have the .NET SDK installed on your computer.

You can install the .NET SDK using homebrew on the command line: `brew install --cask dotnet-sdk`

Alternatively, you can download the .NET SDK [here](https://dotnet.microsoft.com/en-us/)

To start up the API locally, use the command `dotnet run` from the CLI when inside the `FirstWebApp/` directory
