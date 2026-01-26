using Clinic.Api.Services.Implementations;
using Clinic.Api.Services.Interfaces;
using Clinic.Infrastructure.Data;

using Clinic.Infrastructure.Extensions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;


//Using Global Exception Handler:
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        // nếu muốn enum Gender hiện string (Male/Female) thay vì số:
        // o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString("2004-01-29")
    });
});

//Infrastructure
var cs = builder.Configuration.GetConnectionString("ClinicDB");
Console.WriteLine("=== ClinicDB CS === " + cs);

builder.Services.AddInfrastructure(builder.Configuration);


//Add Dependency Injection
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
builder.Services.AddScoped<IRoleService, RoleService>();

var app = builder.Build();


//Configure Global Exception Handler Middleware:

app.UseExceptionHandler(erroraApp =>
{
    erroraApp.Run(async context =>
    {
        var exception = context.Features
        .Get<IExceptionHandlerFeature>()?.Error;

        int statusCode;
        string message;

        switch (exception)
        {
            case KeyNotFoundException:
                statusCode = StatusCodes.Status404NotFound; // 404
                message = exception.Message;
                break;

            case InvalidOperationException:
                statusCode = StatusCodes.Status409Conflict; // 409
                message = exception.Message;
                break;

            case ArgumentException:
                statusCode = StatusCodes.Status400BadRequest; // 400
                message = exception.Message;
                break;

            default:
                statusCode = StatusCodes.Status500InternalServerError; // 500
                message = "An unexpected error occurred.";
                break;
        }
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            statusCode = statusCode,
            error = message
        });
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//SEEDER
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClinicDbContext>();
    await ClinicDbSeeder.SeedAsync(db);
}

app.MapControllers();
app.Run();

