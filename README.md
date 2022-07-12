# Second ASP.NET Core REST API

## Introduction

This project marks the second step in my learning journey towards building a REST API with ASP.NET Core. The API allows for multiple shopping lists to be created, and each of these lists can itself contain multiple items.

Following along with [Part 2. "Retrieving Data" from the linkedin learning course "Building Web APIs with ASP.NET Core in .NET 6"](https://www.linkedin.com/learning/building-web-apis-with-asp-dot-net-core-in-dot-net-6/building-web-apis) allowed me to set up Entity Framework for the first time and create a very simple, two resource, in-memory database. I then set about adapting the API to make use of this new DbContext.

## Learning for this milestone

### Setting up Entity Framework
This was a fascinating learning journey, as I had never used an ORM before. I learned about the following methods which can be used to declaratively set out the one->many relationships which exist within the database.
```
modelBuilder.Entity<ShoppingList>()
    .HasMany(list => list.Items)
    .WithOne(item => item.ShoppingList)
    .HasForeignKey(item => item.ShoppingListId);
```
I also created some seed data using the `.HasData` method.

Finally, I just needed to inject the DbContext into my application using the Service Provider like this:
```
builder.Services.AddDbContext<ShoppingContext>(options =>
    {
        options.UseInMemoryDatabase("Shopping Lists");
    })
```

Luckily, since I already had experienced injecting an in-memory repository that I had build myself, I had a rough idea of what was going on here and what I was achieving by adding this line in my Program.cs file.

### Asynchronous ActionResults with Error Codes

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

Not sure what they'll be yet

## Installation
In order to run the program, you will need to have the .NET SDK installed on your computer.

You can install the .NET SDK using homebrew on the command line: `brew install --cask dotnet-sdk`

Alternatively, you can download the .NET SDK [here](https://dotnet.microsoft.com/en-us/)

To start up the API locally, use the command `dotnet run` from the CLI when inside the `FirstWebApp/` directory
