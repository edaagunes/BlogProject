using Microsoft.AspNetCore.Mvc;
using SensiveProject.BusinessLayer.Abstract;

namespace SensiveProject.PresentationLayer.Controllers
{
	public class DefaultController : Controller
	{
		private readonly IArticleService _articleService;

		public DefaultController(IArticleService articleService)
		{
			_articleService = articleService;
		}

		public IActionResult ArticleList()
		{
			var values = _articleService.TArticleListWithCategoryAndAppUser();
			return View(values);
		}
	}
}
