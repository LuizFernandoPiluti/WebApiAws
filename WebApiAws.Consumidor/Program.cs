using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiAws.Consumidor;
using WebApiAws.Dominio.Settings;
using WebApiAws.Infra.Aws;
using WebApiAws.Servico.Interfaces;
using WebApiAws.Servico.servicos;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<Settings>(
    builder.Configuration.GetSection("Settings"));


builder.Services.AddScoped<IAwsService, AwsService>();
builder.Services.AddScoped<IClientAwsService, ClientAwsService>();


builder.Services.AddHostedService<Worker>();

var app = builder.Build();


app.Run();

