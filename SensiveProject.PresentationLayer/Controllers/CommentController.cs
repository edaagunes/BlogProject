using Microsoft.AspNetCore.Mvc;
using SensiveProject.BusinessLayer.Abstract;
using SensiveProject.EntityLayer.Concrete;

namespace SensiveProject.PresentationLayer.Controllers
{
	public class CommentController : Controller
	{
		private readonly ICommentService _commentService;

		public CommentController(ICommentService commentService)
		{
			_commentService = commentService;
		}

		[HttpGet]
		public PartialViewResult AddComment()
		{
			return PartialView();
		}

		[HttpPost]
		public IActionResult AddComment(Comment comment)
		{
			comment.CreatedDate = DateTime.Now;
			//comment.ArticleId = 0;
			comment.AppUserId = 1;
			_commentService.TInsert(comment);

			return RedirectToAction("ArticleList", "Default");
		}
	}
}
