using libre_pansador_api.Controllers;
using libre_pansador_api.Converters;
using libre_pansador_api.Loyverse;
using libre_pansador_api.Services;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using System.Configuration;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

const string AllowSpecificOrigins = "AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

var emailSection = builder.Configuration.GetSection("Email");
var senderMailboxAddress = new MailboxAddress(emailSection["LogSenderName"], emailSection["LogSenderEmail"]);
var recipientMailboxAddress = new MailboxAddress(emailSection["LogRecipientName"], emailSection["LogRecipientEmail"]);

builder.Services.AddSingleton(new EmailService(senderMailboxAddress, emailSection["LogSenderEmailPassword"] ?? string.Empty));
builder.Services.AddSingleton(recipientMailboxAddress);
builder.Services.AddScoped<libre_pansador_api.CRUD.Customers>();

var jwtSettings = builder.Configuration.GetSection("Jwt");

string? secretKey = jwtSettings["SecretKey"];
if (string.IsNullOrEmpty(jwtSettings["SecretKey"]))
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSpecificOrigins, 
    policy =>
    {
        policy.WithOrigins("https://localhost:443", "http://localhost:80", "https://localhost")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddHttpClient<LoyverseApiClient>(client =>
{
    var accessToken = builder.Configuration["Loyverse:AccessToken"];
    client.BaseAddress = new Uri("https://api.loyverse.com/");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
});

builder.Services.AddControllers()
    .AddApplicationPart(typeof(LoyverseController).Assembly)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
    });
builder.Services.AddControllers()
    .AddApplicationPart(typeof(ErrorLogsController).Assembly);

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
