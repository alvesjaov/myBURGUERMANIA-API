using Microsoft.EntityFrameworkCore;
using myBURGUERMANIA_API.Data;
using myBURGUERMANIA_API.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adicione o serviço CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200" , "https://my-burguermania.vercel.app/") // Substitua pela URL do frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Alteração para MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not found.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString, mysqlOptions =>
    {
        mysqlOptions.EnableRetryOnFailure();
        mysqlOptions.CommandTimeout(60); // Ajuste o tempo limite de conexão para 60 segundos
    })); // UseMySQL ao invés de UseMySql

// Adicionando configuração para ignorar referências circulares
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>(); // Alterado para Scoped
builder.Services.AddScoped<OrderService>(); 
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<StatusService>();
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
