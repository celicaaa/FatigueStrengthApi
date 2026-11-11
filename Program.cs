using FatigueStrengthApi;
using FatigueStrengthApi.Data;
using FatigueStrengthApi.Models;
using FatigueStrengthApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры
builder.Services.AddControllers();

// Подключаем базу данных SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрируем сервисы приложения
builder.Services.AddScoped<FatiguePredictionService>();
builder.Services.AddScoped<PrintExperimentService>();
builder.Services.AddScoped<OptimizationRecommendationsService>();
builder.Services.AddScoped<ReportsService>();
builder.Services.AddScoped<AuthService>();

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

// Перенаправление на HTTPS
app.UseHttpsRedirection();

// Сначала проверяем токен, затем права доступа
app.UseAuthentication();
app.UseAuthorization();

// Подключаем контроллеры
app.MapControllers();

app.Run();
