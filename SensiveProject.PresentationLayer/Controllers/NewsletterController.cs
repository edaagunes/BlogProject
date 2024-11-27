using Microsoft.AspNetCore.Mvc;
using SensiveProject.BusinessLayer.Abstract;
using SensiveProject.EntityLayer.Concrete;

namespace SensiveProject.PresentationLayer.Controllers
{
	public class NewsletterController : Controller
	{
		private readonly INewsletterService _newsletterService;

		public NewsletterController(INewsletterService newsletterService)
		{
			_newsletterService = newsletterService;
		}

		[HttpGet]
		public PartialViewResult Subscribe()
		{
			return PartialView();
		}
		[HttpPost]
		public IActionResult Subscribe(NewsLetter newsLetter)
		{
			_newsletterService.TInsert(newsLetter);
			// burada gittigi sayfa degisecek
			return RedirectToAction("Index","Contact");
		}
	}
}
