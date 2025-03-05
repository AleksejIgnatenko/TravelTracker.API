using Microsoft.EntityFrameworkCore;
using Serilog;
using TravelTracker.API.Middlewares;
using TravelTracker.Application.Services;
using TravelTracker.Core.Abstractions;
using TravelTracker.DataAccess.Context;
using TravelTracker.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddTransient<ExceptionHandlerMiddleware>();

builder.Services.AddDbContext<TravelTrackerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddScoped<IAdvanceReportService, AdvanceReportService>();
builder.Services.AddScoped<IAdvanceReportRepository, AdvanceReportRepository>();

builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICityRepository, CityRepository>();

builder.Services.AddScoped<ICommandService, CommandService>();
builder.Services.AddScoped<ICommandRepository, CommandRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<ITripCertificateService, TripCertificateService>();
builder.Services.AddScoped<ITripCertificateRepository, TripCertificateRepository>();

builder.Services.AddScoped<ITripExpenseService, TripExpenseService>();
builder.Services.AddScoped<ITripExpenseTypeRepository, TripExpenseTypeRepository>();

builder.Services.AddScoped<ITripExpenseTypeService, TripExpenseTypeService>();
builder.Services.AddScoped<ITripExpenseRepository, TripExpenseRepository>();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.UseCors(x =>
{
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http://localhost:3000");
    x.WithMethods().AllowAnyMethod();
});

app.Run();
