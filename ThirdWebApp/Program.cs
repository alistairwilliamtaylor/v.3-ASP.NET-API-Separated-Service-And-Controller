using FirstWebApp;
using FirstWebApp.Models;
using FirstWebApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// We could do other builder configuration up here hypothetically

// THIS IS ALL ADDING DEPENDENCY INJECTION
// Add services to the container.
builder.Services.AddDbContext<ShoppingContext>(options =>
    {
        options.UseSqlite(@"Data Source=/Users/Alistair.Taylor/Documents/SQLiteDbs/ShoppingLists.db");
    })
    .AddScoped<ItemService>() // this importantly needs to be Scoped, not Singleton, because then it will capture the DBCOntext (I can try screwing this up to check the error/warning log)
    .AddControllers()
    // .ConfigureApiBehaviorOptions(options =>
    // {
    //     options.SuppressModelStateInvalidFilter = true;
    // })
    .AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     // app.UseSwagger();
//     // app.UseSwaggerUI();
// }

// THIS IS THE MIDDLEWARE DOWN HERE, AFTER THE BUILD HAS HAPPENED
// THE ORDER IS IMPORTANT IN THIS PIPELINE OF MIDDLEWARE

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();