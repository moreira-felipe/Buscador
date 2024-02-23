using Buscador.Application.Services;
using Buscador.Application.Services.Interfaces;
using Buscador.Configurations;
using Buscador.Domain.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration
	.SetBasePath(builder.Environment.ContentRootPath)
	.AddJsonFile("appsettings.json", true, true)
	.AddEnvironmentVariables();
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));
builder.Services.AddElasticSearch(builder.Configuration);

builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddSingleton(builder.Configuration);

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
