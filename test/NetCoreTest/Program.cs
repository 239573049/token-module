using Token.Module.Extensions;
using NetCoreTest;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTagApplication<NetCoreTestModule>();
var app = builder.Build();

app.InitializeApplication();

app.Run();