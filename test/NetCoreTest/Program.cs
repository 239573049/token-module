
using NetCoreTest;
using Token.Module.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
await builder.Services.AddModuleApplication<NetCoreTestModule>();
var app = builder.Build();

app.MapControllers();

app.InitializeApplication();


app.Run();