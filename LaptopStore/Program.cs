using LaptopStore.Models;
using LaptopStore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
string connectionString;
IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true).Build();
connectionString = config["ConnectionStrings:LaptopStoreConnect"];
// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<LaptopStoreContext>(x =>
{
	x.UseSqlServer(connectionString);
	x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<IBrandRepo,BrandRepo>();
builder.Services.AddScoped<LaptopStoreContext,LaptopStoreContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Access}/{action=Login}/{id?}");


app.Run();
