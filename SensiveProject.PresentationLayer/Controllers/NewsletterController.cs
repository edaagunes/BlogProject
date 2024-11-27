using Microsoft.AspNetCore.Mvc;
using SensiveProject.EntityLayer.Concrete;

namespace SensiveProject.PresentationLayer.Controllers
{
	public class NewsletterController : Controller
	{

		[HttpGet]
		public PartialViewResult Subscribe()
		{
			return PartialView();
		}
		[HttpPost]
		public IActionResult Subscribe(NewsLetter newsLetter)
		{
			// burada gittigi sayfa degisecek
			return RedirectToAction("Index","Category");
		}
	}
}
