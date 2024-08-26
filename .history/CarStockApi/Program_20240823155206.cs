using CarStockApi.Data; 
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder=WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(); // Resister controllers
builder.Services.AddEndpointsApiExplorer(); // Required for minimal APIs
builder.Services.AddSwaggerGen(); // Add Swagger generator

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.UseSwagger(); // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger(); // Enable middleware to serve Swagger UI, specifying the Swagger JSON endpoint.
}

app.UseRouting();
app.UseAuthorization();
app.MapContro