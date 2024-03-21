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

builder.Services.AddCors();

var app = builder.Build();

/*  Configure the HTTP request pipeline.We can add middleware inside this pipeline against that request,
 and do something with this request */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
});
app.UseAuthorization();

app.MapControllers(); //The API knows where to send the requests because we add a route configuration for ou controllers here

/* We can create our database by coding...
 */
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

try
{
    context.Database.Migrate(); //Creates a database if it does not already exist.
    DbInitializer.Initialize(context); /* Because we created a static class, we can just use the Initialize method created and pass it the context. 
    We can drop our database to see if it works. dotnet ef database drop, after that, dotnet run to succesfully seed the db */
}
catch (Exception ex)
{
    logger.LogError(ex, "A problem occured during migration");
}
app.Run();
