
using Microsoft.EntityFrameworkCore;
using Travel_Agency_Management_System.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TravelDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("TravelCS")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Clients}/{action=Index}/{id?}");
app.Run();
