using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BookCatalog.API.Models;
using BookCatalog.Persistence;
using BookCatalog.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        //,b => b.MigrationsAssembly("BookCatalog.Persistence")
    ));

// registra solo el ensamblado donde está MappingProfile
builder.Services.AddAutoMapper(new[] { typeof(BookCatalog.API.Models.MappingProfile).Assembly });

builder.Services.AddTransient<BookRepository>();
builder.Services.AddTransient<KeywordRepository>();
builder.Services.AddTransient<UnitOfWork>();    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookCatalog API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
