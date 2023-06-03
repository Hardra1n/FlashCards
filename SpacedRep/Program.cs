using Microsoft.EntityFrameworkCore;
using SpacedRep.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
// Add services to the container.

builder.Services.AddDbContext<SpacedRepDbContext>((opts) =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SpacedRepDb"));
});

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
