using PTA.BL.Clients;
using PTA.BL.Contracts;
using PTA.BL.DependencyInjection;

using PTA.BL.Services;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext with scoped lifetime
builder.Services.AddBusinessLogic(builder.Configuration);

// Add services to the container.
builder.Services.AddScoped<IMarketPartiesService, MarketPartiesService>();
builder.Services.AddHttpClient<IEsettHttpClient, EsettHttpClient>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();