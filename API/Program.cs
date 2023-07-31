using API.Endpoints;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterPersistenceDependencies(builder.Configuration);
var app = builder.Build();

app.MapGet("/shows", ShowEndpoints.GetShows);

app.Run();
