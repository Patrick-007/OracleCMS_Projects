using CarStockApi.Data; 
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder=WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(); // Resister controllers
builder.Services.AddEndpointsApiExplorer(); // Required for minimal APIs
builder.Services.AddSwaggerGen(); // Add S