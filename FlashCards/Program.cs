using Common;
using FlashCards;
using FlashCards.Data;
using FlashCards.Models.Repositories;
using FlashCards.RpcClients;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory())!.ToString())
    .AddJsonFile("Common/RpcClients.json")
    .AddJsonFile("Common/RpcClients.Development.json")
    .AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FlashCardsDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("FlashCardsDb"));
});

builder.Services.AddScoped<ICardListRepository, EFCardListRepository>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
DbInitializer.Initialize(app);
app.Run();
