using Shop.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddData();
var app = builder.Build();

app.MapEndpoints();

app.Run();
