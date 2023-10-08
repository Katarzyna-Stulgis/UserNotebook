using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UserNotebook.Api.Validators;
using UserNotebook.Dal.Context;
using UserNotebook.Dal.Repositories;
using UserNotebook.Domain.Interfaces;
using UserNotebook.Domain.Models.Entities;
using UserNotebook.Service.DataServices;
using UserNotebook.Service.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserNotebookDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy(name: "UserNotebookOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));

builder.Services.AddAutoMapper(
    typeof(Program),
    typeof(UserProfile)
    );

// DbContext
builder.Services.AddTransient<DbContext, UserNotebookDbContext>();

// Add fluentValidation
builder.Services.AddTransient<IValidator<User>, UserValidator>();

// Services Injection
builder.Services.AddScoped<INotebookService, NotebookService>();

// Repositories Injection
builder.Services.AddScoped<INotebookRepository, NotebookRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("UserNotebookOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
