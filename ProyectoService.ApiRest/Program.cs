using Microsoft.EntityFrameworkCore;
using ProyectoService.AccesoDatos;
using ProyectoService.AccesoDatos.EntityFramework;
using ProyectoService.Aplicacion.CasosUso;
using ProyectoService.Aplicacion.ICasosUso;
using ProyectoService.LogicaNegocio.IRepositorios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProyectoServiceContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ProyectoServiceContext"));
});

//repositorios 
builder.Services.AddScoped<IClienteRepositorio,ClienteEFRepositorio>();
// casos de uso
builder.Services.AddScoped<IAgregarClienteUC, AgregarClienteUC>();
builder.Services.AddScoped<IObtenerTodosLosClientesUC,ObtenerTodosLosClientesUC>();
// Add services to the container.

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
