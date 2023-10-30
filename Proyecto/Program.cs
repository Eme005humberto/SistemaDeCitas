using CurrieTechnologies.Razor.SweetAlert2;
using IUCliente.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Proyecto;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5134") });
//Usamos nuestros servicios mas concretamente Las clases he interfaces
builder.Services.AddScoped<ICitasService, CitasService>();
builder.Services.AddScoped<IPacientesService, PacientesServices>();
//Agregamos los servicios de Sweet Alert
builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
