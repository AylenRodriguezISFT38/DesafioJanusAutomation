using Microsoft.EntityFrameworkCore;
using System;
using TestJannusAutomation.Models;
using TestJannusAutomation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<IntervalTaskService>();
builder.Services.AddDbContext<TestContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProductoConnection")));
builder.Services.AddCors(p => p.AddPolicy("Policy",builder => builder
    .AllowAnyOrigin()
    .WithMethods("GET", "PUT","POST","DELETE")
    .AllowAnyMethod()
    .AllowAnyHeader()
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Policy");
app.UseAuthorization();

app.MapControllers();

app.Run();
