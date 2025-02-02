using FluentValidation.AspNetCore;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Serilog;
using Tanks.Application.Interfaces;
using Tanks.Application.Services;
using Tanks.Common.Validation;
using Tanks.Infrastructure.Interfaces;
using Tanks.Infrastructure.MongoDB;
using Tanks.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddScoped<ITankRepository, TankRepository>();
builder.Services.AddScoped<IBattleRepository, BattleRepository>();
builder.Services.AddScoped<ITankService, TankService>();
builder.Services.AddScoped<IBattleService, BattleService>();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TankValidator>());

builder.Services.AddHealthChecks();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

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

// Add health check endpoint
app.MapHealthChecks("/health");

app.Run();