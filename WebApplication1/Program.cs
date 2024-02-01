using Application.Services;
using Domain.Options;
using Domain.Requests;
using Domain.Validators;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TokenOptions>(
    builder.Configuration.GetSection(TokenOptions.Section));

builder.Services.Configure<PasswordHashOptions>(
    builder.Configuration.GetSection(PasswordHashOptions.Section));

builder.Services.AddCors(config =>
{
    config.AddPolicy("AllowOrigin", options => options
                                                 .AllowAnyOrigin()
                                                 .AllowAnyMethod());
});

var provider = builder.Services.BuildServiceProvider();
var tokenOptions = provider.GetRequiredService<IOptions<TokenOptions>>();

var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Value.Key!));
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
      .AddJwtBearer(options =>
      {
          options.RequireHttpsMetadata = false;
          options.SaveToken = true;

          options.TokenValidationParameters = new TokenValidationParameters
          {
              IssuerSigningKey = securityKey,
              ValidateIssuerSigningKey = true,

              ValidateAudience = true,
              ValidAudience = tokenOptions.Value.Audience,
              ValidateIssuer = true,
              ValidIssuer = tokenOptions.Value.Issuer,
              ValidateLifetime = true
          };
      });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

// Add services to the container.
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add(typeof(CustomActionFilterAttribute));
//});

builder.Services.AddControllers();

// Desativa mensagem de erros automáticos do .NET
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDatabase"));
});

builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IValidator<BaseCarRequest>, CarValidator>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IValidator<BaseUserRequest>, UserValidator>();

var app = builder.Build();

// Middlewares
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");
//app.UseMiddleware<ApiKeyMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

var services = builder.Services.BuildServiceProvider();
var context = services.GetRequiredService<Context>();

await context.Database.EnsureCreatedAsync();

app.Run();
