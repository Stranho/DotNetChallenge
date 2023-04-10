using Blazored.Modal;
using MassTransit;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using SimpleChat.Areas.Identity;
using SimpleChat.Data;
using SimpleChat.Helpers;
using SimpleChat.Hubs;
using SimpleChat.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
builder.Services.AddIdentityCore<ApplicationUser>();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddScoped<ChatRoomService>();
builder.Services.AddMudServices(c => { c.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomRight; });
builder.Services.AddSignalR();
builder.Services.AddSingleton<SharedData>();
builder.Services.AddBlazoredModal();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapHub<ChatHub>("/chathub");
var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.ReceiveEndpoint("chatbot-event", e =>
    {
        ChatBotConsumer._context = app.Services.GetRequiredService<IHubContext<ChatHub>>();
        e.Consumer<ChatBotConsumer>();
    });
});
await busControl.StartAsync();

app.Run();
