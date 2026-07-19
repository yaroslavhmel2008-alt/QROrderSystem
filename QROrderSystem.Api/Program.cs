using System.Reflection;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using QROrderSystem.Application.Common.Behaviors;
using QROrderSystem.Application.Interfaces.Persistence;
using QROrderSystem.Application.Interfaces.Repositories;
using QROrderSystem.Application.Interfaces.Services;
using QROrderSystem.Infrastructure.Services; 
using QROrderSystem.Application.Mappings;
using QROrderSystem.Infrastructure.Middleware;
using QROrderSystem.Infrastructure.Persistence;
using QROrderSystem.Infrastructure.Repositories;
using System.Text.Json.Serialization;
using QROrderSystem.Infrastructure.Hubs;


var builder = WebApplication.CreateBuilder(args);

// 1. ДОДАЄМО КОНТРОЛЕРИ
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        // options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(Assembly.Load("QROrderSystem.Application"));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(Assembly.Load("QROrderSystem.Application"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 2. ПРАВИЛЬНА РЕЄСТРАЦІЯ БАЗИ ДАНИХ
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Рядок підключення має бути у файлі appsettings.json під назвою "DefaultConnection"
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 3. РЕЄСТРАЦІЯ СЕРВІСІВ
builder.Services.AddScoped<ICategoryService, CategoryService>(); 
builder.Services.AddScoped<ILocationService, LocationService>(); 
builder.Services.AddScoped<IProductService, ProductService>();   
builder.Services.AddScoped<IOrderService, OrderService>();       
builder.Services.AddScoped<IOrderItemService, OrderItemService>();

// Реєстрація Репозиторіїв та UnitOfWork
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.WithOrigins("http://192.168.0.65:5173", "http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("AllowAll");
app.MapHub<OrderHub>("/orderHub");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();