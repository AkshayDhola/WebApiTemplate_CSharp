using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using Web.Template.Helpers;
using Web.Template.Middleware;
using Web.Template.Services;
using Web.Template.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IWeatherForecast,WeatherForecastServices>();

EmailHelper.Configure(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(error =>
{
    error.Run(async context =>
    {
        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
       
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
 

        var errorResponse = new
        {
            error = ex?.InnerException?.Message ?? ex?.Message,
            type = ex?.GetType().Name
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    });
});


app.UseRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
