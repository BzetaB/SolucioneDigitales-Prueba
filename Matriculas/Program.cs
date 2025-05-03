using Matriculas.Persistence.Context;
using Matriculas.Service.Implementation;
using Matriculas.Service.Interface;
using Matriculas.Utils.Mappers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    //Context
var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<AppContextDB>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IMatriculaService, MatriculaService>();

//Mappers
builder.Services.AddAutoMapper(typeof(EstudianteProfile));
builder.Services.AddAutoMapper(typeof(CursoProfile));
builder.Services.AddAutoMapper(typeof(MatriculaProfile));

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
