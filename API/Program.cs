using API.Endpoints;
using Application;
using Caching;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterApplicationDependencies();
builder.Services.RegisterCachingDependencies(builder.Configuration);
builder.Services.RegisterPersistenceDependencies(builder.Configuration);
var app = builder.Build();

app.MapGet("/shows", ShowEndpoints.GetShowsAsync);

app.Run();
