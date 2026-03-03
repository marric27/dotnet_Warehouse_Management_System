using dotnet_Warehouse_Management_System.Common.Exceptions;
using dotnet_Warehouse_Management_System.Customers.Entities.Repository;
using dotnet_Warehouse_Management_System.Customers.Entities.Services;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn;
using dotnet_Warehouse_Management_System.GoodsIn.CheckGoodsIn.Services;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Services;
using dotnet_Warehouse_Management_System.GoodsIn.Putaway.Services;
using dotnet_Warehouse_Management_System.GoodsIn.Receiving;
using dotnet_Warehouse_Management_System.GoodsIn.Services;
using dotnet_Warehouse_Management_System.Outbound.Entities.Repositories;
using dotnet_Warehouse_Management_System.Outbound.Entities.Services;
using dotnet_Warehouse_Management_System.Outbound.Release.Services;
using dotnet_Warehouse_Management_System.Outbound.SalesOrders.Services;
using dotnet_Warehouse_Management_System.Picking.Entities.Repository;
using dotnet_Warehouse_Management_System.Picking.Entities.Service;
using dotnet_Warehouse_Management_System.Picking.Services;
using dotnet_Warehouse_Management_System.Products.Entities.Repository;
using dotnet_Warehouse_Management_System.Products.Entities.Services;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddOpenApi();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors
                        .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Invalid value." : e.ErrorMessage)
                        .ToArray()
                );

            return new BadRequestObjectResult(new ValidationProblemDetails(errors)
            {
                Title = "Validation failed",
                Status = StatusCodes.Status400BadRequest,
                Detail = "One or more request fields are invalid. Check the errors property for details."
            });
        };
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISlotService, SlotService>();
builder.Services.AddScoped<ISlotRepository, SlotRepository>();
builder.Services.AddScoped<IGrnRepository, GrnRepository>();
builder.Services.AddScoped<IGrnItemRepository, GrnItemRepository>();
builder.Services.AddScoped<IGrnService, GrnService>();
builder.Services.AddScoped<IGrnItemService, GrnItemService>();
builder.Services.AddScoped<ReceivingService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IGrnItemStateService, GrnItemStateService>();
builder.Services.AddScoped<ISalesOrderLineRepository, SalesOrderLineRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<SalesOrderService>();
builder.Services.AddScoped<PicklistGenService>();
builder.Services.AddScoped<IPicklistService, PicklistService>();
builder.Services.AddScoped<IPicklistRepository, PicklistRepository>();
builder.Services.AddScoped<IPicklistItemRepository, PicklistItemRepository>();
builder.Services.AddScoped<IPicklistItemService, PicklistItemService>();
builder.Services.AddScoped<IPickingInfoRepository, PickingInfoRepository>();
builder.Services.AddScoped<IPickingInfoService, PickingInfoService>();
builder.Services.AddScoped<PickingService>();
builder.Services.AddScoped<IStockUnitService, StockUnitService>();
builder.Services.AddScoped<IStockUnitRepository, StockUnitRepository>();
builder.Services.AddScoped<ICheckingInfoRepository, CheckingInfoRepository>();
builder.Services.AddScoped<ICheckingInfoService, CheckingInfoService>();
builder.Services.AddScoped<CheckGoodsInService>();
builder.Services.AddScoped<PutawayService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAngularDev");
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionFeature?.Error;

        var (status, title) = exception switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Invalid request"),
            DomainException => (StatusCodes.Status409Conflict, "Domain conflict"),
            _ => (StatusCodes.Status500InternalServerError, "Unexpected error")
        };

        var problemDetails = new ProblemDetails
        {
            Type = $"https://httpstatuses.com/{status}",
            Title = title,
            Status = status,
            Detail = exception?.Message ?? "An unexpected error occurred."
        };
        problemDetails.Extensions["traceId"] = context.TraceIdentifier;

        context.Response.StatusCode = status;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});
app.UseAuthorization();

app.UseSerilogRequestLogging(); // log automatico delle request HTTP

app.MapControllers();

app.Run();
