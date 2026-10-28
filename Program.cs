using FatigueStrengthApi.Services;
using FatigueStrengthApi.Data;
using FatigueStrengthApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FatigueStrengthApi;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры
builder.Services.AddControllers();

// Подключаем базу данных SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Подключаем сервис прогнозирования
builder.Services.AddScoped<FatiguePredictionService>();

// Настраиваем проверку JWT-токенов
builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = AuthOptions.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.SigningKey,

                ValidateLifetime = true
            };
    });

var app = builder.Build();

app.UseHttpsRedirection();

// Сначала проверяем токен, затем права доступа
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
