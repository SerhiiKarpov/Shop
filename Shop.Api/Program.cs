using Shop.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();
var app = builder.Build();

app.MapEndpoints();

app.Run();
