using FluentValidation.AspNetCore;
using SensiveProject.BusinessLayer.Abstract;
using SensiveProject.BusinessLayer.Concrete;
using SensiveProject.BusinessLayer.Container;
using SensiveProject.DataAccessLayer.Abstract;
using SensiveProject.DataAccessLayer.Context;
using SensiveProject.DataAccessLayer.EntityFramework;
using SensiveProject.EntityLayer.Concrete;
using SensiveProject.PresentationLayer.Models;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);




// Generic Repository yi kullanabilmek için

builder.Services.AddDbContext<SensiveContext>();

// Add services to the container.
// Identity kütüphanesini kullanmamýza izin verir. DI kullanacaðýmýzý bildirdik.

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<SensiveContext>().AddErrorDescriber<CustomIdentityValidator>();

builder.Services.AddControllersWithViews().AddFluentValidation();
builder.Services.ContainerDependencies();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();

	builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); //Login icin
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
});

app.Run();
