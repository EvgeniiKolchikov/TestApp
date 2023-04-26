using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Interfaces;
using WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("InMemoryDb");

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase(connection));
builder.Services.AddTransient<IUserRepository, UserRepository>();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();