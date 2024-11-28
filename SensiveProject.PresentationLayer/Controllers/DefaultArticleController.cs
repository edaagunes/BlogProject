using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SensiveProject.BusinessLayer.Abstract;
using SensiveProject.EntityLayer.Concrete;
using SensiveProject.PresentationLayer.Models;

namespace SensiveProject.PresentationLayer.Controllers
{

	public class DefaultArticleController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly ICommentService _commentService;
		private readonly IArticleTagCloudService _articleTagCloudService;
		private readonly UserManager<AppUser> _userManager;

		public DefaultArticleController(IArticleService articleService, UserManager<AppUser> userManager, ICommentService commentService, IArticleTagCloudService articleTagCloudService)
		{
			_articleService = articleService;
			_userManager = userManager;
			_commentService = commentService;
			_articleTagCloudService = articleTagCloudService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> ArticleDetail(int id)
		{
			ViewData["PageTitle"] = "Blog Detayı";

			// Giriş yapan kullanıcı bilgisi
			if (User.Identity.IsAuthenticated)
			{
				var user = await _userManager.FindByNameAsync(User.Identity.Name);
				ViewBag.userId = user.Id;
			}
			else
			{
				ViewBag.userId = null;
			}

			// Geçerli blog detayını al
			var currentArticle = _articleService.TGetById(id);

			// Blog sahibi bilgisi kontrolü
			if (currentArticle != null && currentArticle.AppUser != null)
			{
				ViewBag.AuthorName = $"{currentArticle.AppUser.Name} {currentArticle.AppUser.Surname}";
				ViewBag.AuthorImage = currentArticle.AppUser.ImageUrl;
			}

			// Blogun yorumlarını ekle
			currentArticle.Comments = _commentService.TGetCommentsByArticleId(id);


			// Önceki blog yazısını al
			var previousArticle = _articleService.TGetAll()
				.Where(a => a.ArticleId < id)
				.OrderByDescending(a => a.ArticleId)
				.FirstOrDefault();

			// Sonraki blog yazısını al
			var nextArticle = _articleService.TGetAll()
				.Where(a => a.ArticleId > id)
				.OrderBy(a => a.ArticleId)
				.FirstOrDefault();

			// ViewBag ile view'e aktar
			ViewBag.PreviousArticle = previousArticle;
			ViewBag.NextArticle = nextArticle;

			// Article'a ait TagCloud listesi
			var articleTagClouds = _articleTagCloudService.TGetAllByArticleId(id);
			ViewBag.ArticleTags = articleTagClouds.Select(atc => atc.TagCloud.Title).ToList();

			return View(currentArticle);
		}

		//[HttpGet]
		//public IActionResult AddComment(int id)
		//{
		//	return View();
		//}

		//[HttpPost]
		//public IActionResult AddComment(Comment comment)
		//{
		//	return RedirectToAction("ArticleDetail");
		//}
	}
}
