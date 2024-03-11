/* This program.cs class is the starting point of our app */

using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/* Add services to the container.(Dependency Injection Container) when we wanna use a particular service inside of our classes in our project, 
then we're gonna use the facility called dependency injection to inject that service to be available to be used inside our app. */

builder.Services.AddControllers(); /* Because we're using a web api, we use this service api controller. 
We add our controllers to our project inside this container */
builder.Services.AddEndpointsApiExplorer(); //We need these coiuple of services to generate the swagger content we see in the browser
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

/*  Configure the HTTP request pipeline.We can add middleware inside this pipeline against that request,
 and do something with this request */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers(); //The API knows where to send the requests because we add a route configuration for ou controllers here

app.Run();
