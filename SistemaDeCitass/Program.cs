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
//Vamos implemetar el servicio de compartir informacion
builder.Services.AddCors(c =>
{
    c.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
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
app.UseCors("NuevaPolitica");//Agregamos nuestra politica para compartir informacion
//sin ningun tipo de restriccion ya que como app servidor y app cliente se va a ejecutar
//en URL Distintas
app.UseAuthorization();
app.MapControllers();

app.Run();
