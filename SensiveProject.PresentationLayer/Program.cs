using SensiveProject.BusinessLayer.Abstract;
using SensiveProject.BusinessLayer.Concrete;
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

builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<IArticleDal,EfArticleDal>();
builder.Services.AddScoped<IArticleService,ArticleManager>();

builder.Services.AddScoped<ICommentDal,EfCommentDal>();
builder.Services.AddScoped<ICommentService,CommentManager>();

builder.Services.AddScoped<IContactDal,EfContactDal>();
builder.Services.AddScoped<IContactService,ContactManager>();

builder.Services.AddScoped<INewsletterDal, EfNewsletterDal>();
builder.Services.AddScoped<INewsletterService, NewsletterManager>();

builder.Services.AddScoped<ITagCloudDal, EfTagCloudDal>();
builder.Services.AddScoped<ITagCloudService, TagCloudManager>();


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
