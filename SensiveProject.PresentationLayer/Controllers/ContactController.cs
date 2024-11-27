using Microsoft.AspNetCore.Mvc;
using SensiveProject.EntityLayer.Concrete;

namespace SensiveProject.PresentationLayer.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public PartialViewResult SendContactMessage()
		{
			return PartialView();
		}
		[HttpPost]
		public IActionResult SendContactMessage(Contact contact)
		{
			return RedirectToAction("Index","Contact");
		}
	}
}
