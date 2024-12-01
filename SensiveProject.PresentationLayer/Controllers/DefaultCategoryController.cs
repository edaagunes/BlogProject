using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SensiveProject.BusinessLayer.Abstract;


namespace SensiveProject.PresentationLayer.Controllers
{
	[AllowAnonymous]
	public class DefaultCategoryController : Controller
	{
		private readonly IArticleService _articleService;

		public DefaultCategoryController(IArticleService articleService)
		{
			_articleService = articleService;
		}


		public IActionResult Index(int? page)
		{
			ViewData["PageTitle"] = "Kategoriler";

			// Sayfa numarasını al, eğer null ise 1 olarak ayarla
			int pageNumber = page ?? 1; 
			int pageSize = 6;

			var values = _articleService.TArticleListWithCategoryAndAppUser();

			// Sayfalama işlemi
			var pagedArticles = values
				.Skip((pageNumber - 1) * pageSize)  // Sayfada kaç öğe atlanacak
				.Take(pageSize)               // Alınacak öğe sayısı
				.ToList();                    // Listeye dönüştür

			// Sayfa bilgilerini ViewBag ile gönderin
			ViewBag.CurrentPage = pageNumber;

			ViewBag.PageCount = Math.Ceiling(values.Count() / (double)pageSize);

			return View(pagedArticles);
		}

	}
}
