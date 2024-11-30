using Microsoft.AspNetCore.Mvc;
using SensiveProject.BusinessLayer.Abstract;
using SensiveProject.EntityLayer.Concrete;

namespace SensiveProject.PresentationLayer.Controllers
{
	public class DefaultController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly IArticleTagCloudService _articleTagCloudService;
		private readonly ITagCloudService _tagCloudService;

		public DefaultController(IArticleService articleService, IArticleTagCloudService articleTagCloudService, ITagCloudService tagCloudService)
		{
			_articleService = articleService;
			_articleTagCloudService = articleTagCloudService;
			_tagCloudService = tagCloudService;
		}

		public IActionResult HomePage()
		{
			var values = _articleService.TArticleListWithCategoryAndAppUser().ToList();
			foreach (var article in values)
			{
				article.Tags = article.ArticleTagClouds.Select(at => at.TagCloud).ToList(); // Etiketleri modelde bağla
			}
			return View(values);
		}

	}
}
