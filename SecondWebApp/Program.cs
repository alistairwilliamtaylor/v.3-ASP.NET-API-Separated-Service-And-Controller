using FirstWebApp;
using FirstWebApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShoppingContext>(options =>
    {
        options.UseInMemoryDatabase("Shopping Lists");
    })
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();