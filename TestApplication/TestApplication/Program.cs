using Microsoft.EntityFrameworkCore;
using TestApplication.Contexts;
using TestApplication.Interfaces;
using TestApplication.Repositories;

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