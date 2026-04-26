var builder = WebApplication.CreateBuilder(args);
builder.Services.AddData();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
