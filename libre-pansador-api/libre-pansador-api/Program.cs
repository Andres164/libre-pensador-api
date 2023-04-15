using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigins",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:8080")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials()
                                 .SetIsOriginAllowedToAllowWildcardSubdomains()
                                 .SetIsOriginAllowed(origin => true);
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
