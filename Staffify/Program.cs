using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Staffify.Database;
using Staffify.Services;

var builder = WebApplication.CreateBuilder(args);

// Build Logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Log/.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Add Logging
builder.Logging.AddSerilog();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<Database>();
builder.Services.AddSingleton<AppSingletonData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

Log.Information("Webserver starting.");

app.Run();
