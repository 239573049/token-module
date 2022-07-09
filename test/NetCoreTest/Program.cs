using Token.Module.Extensions;
using NetCoreTest;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddModuleApplication<NetCoreTestModule>();
var app = builder.Build();

app.InitializeApplication();

app.MapControllers();

app.Run();