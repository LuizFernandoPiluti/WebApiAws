using WebApiAws.Dominio.Settings;
using WebApiAws.Infra.Aws;
using WebApiAws.Servico.Interfaces;
using WebApiAws.Servico.servicos;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<Settings>(
    builder.Configuration.GetSection("Settings"));


builder.Services.AddScoped<IAwsService, AwsService>();
builder.Services.AddScoped<IClientAwsService, ClientAwsService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger(options => options.OpenApiVersion =
Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);
    app.UseSwaggerUI();

}

app.UseAuthorization();

app.MapControllers();

app.Run();
