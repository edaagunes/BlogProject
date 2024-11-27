using Microsoft.AspNetCore.Mvc;

namespace SensiveProject.PresentationLayer.ViewComponents.DefaultLayout
{
	public class _DefaultContactMapComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
