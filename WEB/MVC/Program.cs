using MVC.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<FlashCardsClient>((client) =>
{
    client.BaseAddress = new Uri(
        builder.Configuration.GetConnectionString("FlashCardsAPI")!);
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
