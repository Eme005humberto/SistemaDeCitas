using SistemaDeCitas.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//Agregamos el servicio de nuestra base de datos
builder.Services.AddDbContext<SistemaCitasContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("CadenaLanox"));
    //Colocamos el nodo que se 
    //encuentra en appsettings
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

app.UseAuthorization();

app.MapControllers();

app.Run();
