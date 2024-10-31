using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SensiveProject.EntityLayer.Concrete;
using SensiveProject.PresentationLayer.Models;

namespace SensiveProject.PresentationLayer.Controllers
{
	public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;

		public LoginController(SignInManager<AppUser> signInManager)
		{
			_signInManager = signInManager;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(LoginViewModel model)
		{
			var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, true);
			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Category");
			}
			else
			{
				return View();
			}
		}
	}
}
