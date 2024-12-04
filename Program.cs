using Microsoft.EntityFrameworkCore;
using myBURGUERMANIA_API.Data;
using myBURGUERMANIA_API.Services;
using myBURGUERMANIA_API.Configurations;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adicione o serviço CORS
builder.Services.AddCorsConfiguration();

// Alteração para MySQL
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// Adicione os serviços
builder.Services.AddServiceConfiguration();

// Adicionando configuração para ignorar referências circulares
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Adiciona esta linha para aplicar as migrações automaticamente
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
    SeedData.Initialize(scope.ServiceProvider); 
}

// Use o middleware CORS
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "myBURGUERMANIA API");
        c.RoutePrefix = string.Empty; // Para acessar o Swagger na raiz (http://localhost:5140/)
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Alterar a porta para evitar conflito
app.Urls.Add("http://*:8080");

await app.RunAsync();
