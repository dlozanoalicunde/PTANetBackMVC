using Microsoft.EntityFrameworkCore;
using BankAPI.DBO;
using BankAPI.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Create connection string for all enviroments
string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                            builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<DataBaseConnection>(options => options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bank API 1.0"));

//Create the context for the dataBase
DbContextOptions<DataBaseConnection>? options = new DbContextOptionsBuilder<DataBaseConnection>()
            .UseSqlServer(connectionString)
            .Options;

//Consume the api and send the info.
var bankConsumer = new BankConsumer(new DataBaseConnection(options));
// Do this async
await bankConsumer.StoreAllBanksInDataBase();

app.Run();


