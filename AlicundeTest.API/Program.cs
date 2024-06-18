using AlicundeTest.API.Infraestructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Serilog configuration with settings
builder.ConfigureSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// DbContext configuration
builder.Services.ConfigureDbContext(configuration);

// Swagger configuration
builder.Services.AddCustomSwagger();

// Mediator, repository and UnitOfWork
builder.Services.CustomServices();

var app = builder.Build();

Log.Information("App starting...");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // Adding swagger middleware
    app.UseCustomSwagger();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");

//Database initializations
app.DatabaseInitialization();

app.Run();
