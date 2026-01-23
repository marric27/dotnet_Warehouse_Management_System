using dotnet_Warehouse_Management_System.Customers.Entities.Repository;
using dotnet_Warehouse_Management_System.Customers.Entities.Services;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;
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
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddOpenApi();

builder.Services.AddControllers()
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
builder.Services.AddScoped<IPickingInfoRepository, PickingInfoRepository>();
builder.Services.AddScoped<IPickingInfoService, PickingInfoService>();
builder.Services.AddScoped<PickingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAngularDev");
app.UseAuthorization();

app.MapControllers();

app.Run();
