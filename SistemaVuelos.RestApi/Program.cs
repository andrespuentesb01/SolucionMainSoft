using SistemaVuelos.IOC;


var builder = WebApplication.CreateBuilder(args);
builder.Services.InyectarDependencia(builder.Configuration);
var app = builder.Build();

app.MapGet("/", () => "Hello NewShore!");

app.Run();
