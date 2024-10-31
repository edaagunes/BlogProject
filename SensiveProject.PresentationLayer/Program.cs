using SensiveProject.DataAccessLayer.Context;
using SensiveProject.EntityLayer.Concrete;
using SensiveProject.PresentationLayer.Models;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);



// Generic Repository yi kullanabilmek için

builder.Services.AddDbContext<SensiveContext>();

// Add services to the container.
// Identity kütüphanesini kullanmamýza izin verir. DI kullanacaðýmýzý bildirdik.

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<SensiveContext>().AddErrorDescriber<CustomIdentityValidator>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); //Login icin
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
