using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SensiveProject.EntityLayer.Concrete;

namespace SensiveProject.PresentationLayer.Areas.Author.Controllers
{
	[Area("Author")]
	[Route("Author/[controller]/[action]/{id?}")]
	public class DashboardController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public DashboardController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			// Giriş yapmış kullanıcı var mı?
			var currentUser = User.Identity.IsAuthenticated ? await _userManager.FindByNameAsync(User.Identity.Name) : null;

			// Giriş yapan kullanıcı varsa, bilgilerini ViewBag'e ekleyin.
			if (currentUser != null)
			{
				ViewBag.userId = currentUser.Id;
				ViewBag.AuthorName = $"{currentUser.Name} {currentUser.Surname}";
				ViewBag.AuthorImage = currentUser.ImageUrl;
			}

			return View();
		}
	}
}
