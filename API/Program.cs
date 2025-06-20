using CarDealership.Domain.Entities;
using CarDealership.Infrastructure.Persistence;
using CarDealership.Infrastructure.Repositories;
using CarDealership.Infrastructure.Security;
using CarDealership.Application.Interfaces.Userinterface;
using CarDealership.Application.Features.Authentication;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using FluentValidation.AspNetCore;
using MediatR;
using System.Text;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// 1) Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2) EF Core — point to your SQL Express instance in appsettings.json
builder.Services.AddDbContext<CarDealershipDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// 3) Password hashing for User
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// 4) MediatR + FluentValidation
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<RegisterUserCommand>();

// 5) Your interfaces → Infrastructure implementations
builder.Services.AddScoped<
    CarDealership.Application.Interfaces.Userinterface.IUserRepository,
    CarDealership.Infrastructure.Repositories.UserRepository>();

builder.Services.AddScoped<
    CarDealership.Application.Interfaces.Userinterface.IJwtTokenService,
    CarDealership.Infrastructure.Security.JwtTokenService>();

// 6) JWT Bearer Authentication
var jwt = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opts =>
{
    opts.RequireHttpsMetadata = true;
    opts.SaveToken = true;
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwt["Key"]))
    };
});

var app = builder.Build();

// 7) Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
