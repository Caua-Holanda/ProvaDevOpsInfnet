using ProvaMedGroup;

var builder = WebApplication.CreateBuilder(args);


var startup = new Startup(builder.Configuration, builder.Environment);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

var provider = app.Services.GetRequiredService<Microsoft.AspNetCore.Mvc.ApiExplorer.IApiVersionDescriptionProvider>();

startup.Configure(app, builder.Environment, provider);

app.Run();
