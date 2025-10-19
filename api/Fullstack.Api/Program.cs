using Fullstack.Api.Data;
using Fullstack.Api.DTOs;
using Fullstack.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// EF Core → SQL Server
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// DI
builder.Services.AddScoped<IProductService, ProductService>();

// CORS (frontend Vite por defecto)
const string CorsPolicy = "web";
builder.Services.AddCors(opt =>
    opt.AddPolicy(CorsPolicy, p => p
        .WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(CorsPolicy);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Health
app.MapGet("/health", () => Results.Ok(new { status = "ok", utc = DateTime.UtcNow }));

// List
app.MapGet("/products", async (int page, int size, IProductService svc) =>
{
    var (items, total) = await svc.ListAsync(page, size);
    return Results.Ok(new { total, page, size, items });
});

// Get by id
app.MapGet("/products/{id:int}", async (int id, IProductService svc) =>
{
    var item = await svc.GetAsync(id);
    return item is null ? Results.NotFound() : Results.Ok(item);
});

// Create
app.MapPost("/products", async ([FromBody] ProductCreateDto dto, IProductService svc) =>
{
    try
    {
        var id = await svc.CreateAsync(dto);
        return Results.Created($"/products/{id}", new { id });
    }
    catch (InvalidOperationException ex)
    {
        return Results.Conflict(new ProblemDetails { Title = "Duplicado", Detail = ex.Message });
    }
});

// Update
app.MapPut("/products/{id:int}", async (int id, ProductCreateDto dto, IProductService svc) =>
{
    try
    {
        var ok = await svc.UpdateAsync(id, dto);
        return ok ? Results.NoContent() : Results.NotFound();
    }
    catch (InvalidOperationException ex)
    {
        return Results.Conflict(new ProblemDetails { Title = "Duplicado", Detail = ex.Message });
    }
});

// Delete
app.MapDelete("/products/{id:int}", async (int id, IProductService svc) =>
{
    var ok = await svc.DeleteAsync(id);
    return ok ? Results.NoContent() : Results.NotFound();
});

app.Run();