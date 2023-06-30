using libre_pensador_api.Controllers;
using libre_pensador_api.Converters;
using libre_pensador_api.Loyverse;
using libre_pensador_api.Services;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using System.Configuration;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using libre_pensador_api.Models;
using Microsoft.EntityFrameworkCore;
using libre_pensador_api.CRUD;
using libre_pensador_api.Interfaces;

const string AllowSpecificOrigins = "AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

var encryptionSettings = builder.Configuration.GetSection("Encryption");
var encryptionKey = encryptionSettings["SecretKey"];
if (string.IsNullOrEmpty(encryptionKey))
    throw new Exception("Encryption key is missing in the configuration.");
EncryptionUtility.Initialize(encryptionKey);

var jwtSettings = builder.Configuration.GetSection("Jwt");
string? secretKey = jwtSettings["SecretKey"];
if (string.IsNullOrEmpty(secretKey))
    throw new Exception("JWT SecretKey is missing in the configuration.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["access_token"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("IsAdmin", "True"));
});

string[] allowedIps = builder.Configuration.GetSection("AllowedIps").Get<List<String>>().ToArray();
IpConverter.ConvertIpsToOrigin(allowedIps);

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSpecificOrigins, 
    policy =>
    {
        policy.WithOrigins(allowedIps) // Allow any origin: SetIsOriginAllowed(origin => true)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddHttpClient<LoyverseCustomersApiClient>(client =>
{
    var accessToken = builder.Configuration["Loyverse:AccessToken"];
    client.BaseAddress = new Uri("https://api.loyverse.com/");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
});

builder.Services.AddDbContextFactory<CafeLibrePensadorDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CafeLibrePensadorDb")));

builder.Services.AddScoped<ILoggingService, LoggingService>();

builder.Services.AddDbContext<CafeLibrePensadorDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CafeLibrePensadorDb")));

builder.Services.AddScoped<ICardsService, Cards>();
builder.Services.AddScoped<ICustomersService, Customers>();
builder.Services.AddScoped<IUserService, Users>();
builder.Services.AddScoped<IExpensesService, Expenses>();
builder.Services.AddScoped<IExpenseCategoriesService, ExpenseCategories>();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(AuthenticationController).Assembly);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CardsController).Assembly);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CustomersController).Assembly);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(ExpensesController).Assembly);

var emailSection = builder.Configuration.GetSection("Email");
var senderMailboxAddress = new MailboxAddress(emailSection["LogSenderName"], emailSection["LogSenderEmail"]);
var recipientMailboxAddress = new MailboxAddress(emailSection["LogRecipientName"], emailSection["LogRecipientEmail"]);
builder.Services.AddSingleton(new EmailService(senderMailboxAddress, emailSection["LogSenderEmailPassword"] ?? string.Empty));
builder.Services.AddSingleton(recipientMailboxAddress);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(ErrorLogsController).Assembly);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(LoyverseCustomersController).Assembly)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
