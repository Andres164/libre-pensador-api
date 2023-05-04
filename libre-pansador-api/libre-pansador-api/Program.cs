using libre_pansador_api.Converters;
using libre_pansador_api.Loyverse;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.Security.Cryptography;

const string AllowSpecificOrigins = "AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddScoped<libre_pansador_api.CRUD.Customers>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
