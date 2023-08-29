using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Include ConnectionString and MySqlContext to the Services

// Get Connection String
var connectionString = builder.Configuration
                                .GetConnectionString("MySQLConnectionString");

builder.Services.AddDbContext<MySqlContext>(options => 
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 1))));

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
