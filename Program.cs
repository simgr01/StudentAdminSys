using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using StudentAdminSys.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StudentDbContext>(opts => {
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:StudentAdminSysConnection"]);
});

builder.Services.AddScoped<IRepository, Repository>();

var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute("pagination",
 "/Page{Page}",
 new { Controller = "Home", action = "Index" });

app.UseRouting();
app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");

Repository.EnsurePopulated(app);

app.Run();