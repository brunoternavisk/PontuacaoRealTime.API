using PontuacaoRealTime.API.Configurations;
using PontuacaoRealTime.API.Repositories;
using PontuacaoRealTime.API.Services;
using PontuacaoRealTime.API.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseConfiguration();
builder.Services.AddScoped<IPontosRepository, PontosRepository>();
builder.Services.AddScoped<IPontosService, PontosService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();