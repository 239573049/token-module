using NetCore.Event;
using Token.Module.Extensions;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddModuleApplication<NetCoreEventModule>();

var app = builder.Build();


app.InitializeApplication();


app.Run();