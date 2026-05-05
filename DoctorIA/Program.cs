using DoctorIA;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

DependencyInjection.AddInfraestructure(builder.Services);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DoctorIA API",
        Version = "v1",
        Description = "API de consultas médicas com IA",
        Contact = new OpenApiContact
        {
            Name = "Rafael Pigatto",
            Email = "rafaelrpigatto@gmail.com"
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "DoctorIA API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/health", () => new { status = "ok", time = DateTime.UtcNow })
    .WithName("HealthCheck")
    .WithOpenApi();

app.Run();