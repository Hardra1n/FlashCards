using Microsoft.EntityFrameworkCore;
using SpacedRep.Data;
using SpacedRep.Models;
using SpacedRep.RpcClients;
using SpacedRep.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory())!.ToString())
    .AddJsonFile("Common/RpcClients.json")
    .AddJsonFile("Common/RpcClients.Development.json")
    .AddEnvironmentVariables();

builder.Services.AddDbContext<SpacedRepDbContext>((opts) =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SpacedRepDb"));
});

builder.Services.AddScoped<IRepetitionRepository, EFRepetitionRepository>();
builder.Services.AddScoped<RepetitionApiService>();
builder.Services.AddScoped<RepetitionRpcService>();
builder.Services.AddScoped<FlashCardsRpcPublisher>();
builder.Services.AddHostedService<RpcConsumerService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

DbInitializer.Initialize(app);
app.Run();
