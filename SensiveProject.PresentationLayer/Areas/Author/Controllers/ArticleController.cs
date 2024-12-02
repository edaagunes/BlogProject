using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SensiveProject.BusinessLayer.Abstract;
using SensiveProject.BusinessLayer.ValidationRules.ArticleValidation;
using SensiveProject.EntityLayer.Concrete;

namespace SensiveProject.PresentationLayer.Areas.Author.Controllers
{
	[Area("Author")]
	[Route("Author/[controller]/[action]/{id?}")]
	public class ArticleController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly UserManager<AppUser> _userManager;
		private readonly ICategoryService _categoryService;


		public ArticleController(IArticleService articleService, UserManager<AppUser> userManager, ICategoryService categoryService)
		{
			_articleService = articleService;
			_userManager = userManager;
			_categoryService = categoryService;
		}

		public async Task<IActionResult> MyArticleList()
		{
			var userValue = await _userManager.FindByNameAsync(User.Identity.Name);
			var articleList = _articleService.TGetArticlesByAppUserId(userValue.Id);
			return View(articleList);
		}

		[HttpGet]
		public IActionResult CreateArticle()
		{
			var categoryList = _categoryService.TGetAll();

			// Category listesi, DropDownList için uygun SelectListItem formatına dönüştürülüyor.
			List<SelectListItem> categoryItems = categoryList.Select(x => new SelectListItem
			{
				Text = x.CategoryName,
				Value = x.CategoryId.ToString() // SelectListItem Value özelliği string olduğu için ToString() kullanıyoruz.
			}).ToList();

			ViewBag.CategoryList = categoryItems; // ViewBag ile kategori listesini gönderiyoruz
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> CreateArticle(Article article)
		{
			// Giriş yapan kullanıcıyı alıyoruz
			var userValue = await _userManager.FindByNameAsync(User.Identity.Name);

			if (userValue == null)
			{
				ModelState.AddModelError("", "Kullanıcı bulunamadı.");
				return View(article);
			}

			article.AppUserId = userValue.Id;
			article.CreatedDate = DateTime.Now;

			CreateArticleValidator validationRules = new CreateArticleValidator();
			ValidationResult result = validationRules.Validate(article);

			if (result.IsValid)
			{
				_articleService.TInsert(article);
				return RedirectToAction("MyArticleList");
			}
			else
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
			}

			// ViewBag.CategoryList'i yeniden dolduruyoruz
			var categoryList = _categoryService.TGetAll();
			List<SelectListItem> categoryItems = categoryList.Select(x => new SelectListItem
			{
				Text = x.CategoryName,
				Value = x.CategoryId.ToString()
			}).ToList();

			ViewBag.CategoryList = categoryItems;

			return View(article);
		}



	}
}
