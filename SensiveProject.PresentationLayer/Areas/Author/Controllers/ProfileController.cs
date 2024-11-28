using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SensiveProject.EntityLayer.Concrete;
using SensiveProject.PresentationLayer.Areas.Author.Models;

namespace SensiveProject.PresentationLayer.Areas.Author.Controllers
{
	[Area("Author")]
	public class ProfileController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public ProfileController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			UserEditViewModel model = new UserEditViewModel();
			model.Surname = user.Surname;
			model.Email = user.Email;
			model.Name = user.Name;
			model.Username = user.UserName;

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Index(UserEditViewModel model)
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			user.Surname = model.Surname;
			user.Name = model.Name;
			user.Email = model.Email;
			user.UserName = model.Username;

			user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				RedirectToAction("Index", "Default");
			}
			else
			{
				return View();
			}
			return View(model);
		}
	}
}
