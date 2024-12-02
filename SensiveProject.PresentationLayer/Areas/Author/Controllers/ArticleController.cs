﻿using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using SensiveProject.BusinessLayer.Abstract;
using SensiveProject.BusinessLayer.ValidationRules.CategoryValidation;
using SensiveProject.EntityLayer.Concrete;
using SensiveProject.PresentationLayer.Areas.Author.Models;

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

			var model = new ArticleCategoryViewModel
			{
				Article = new Article(),
				Category = new Category(),
				CategoryList = categoryList.Select(x => new SelectListItem
				{
					Text = x.CategoryName,
					Value = x.CategoryId.ToString()
				}).ToList()
			};

			model.CategoryList.Insert(0, new SelectListItem { Text = "Kategori seçiniz", Value = "" });
			return View(model);
		}


		[HttpPost]
		public async Task<IActionResult> CreateArticle(ArticleCategoryViewModel model)
		{
			if (model.Article == null)
			{
				ModelState.AddModelError("", "Article is null");
				return View(model);
			}
			// Giriş yapan kullanıcıyı alıyoruz
			var userValue = await _userManager.FindByNameAsync(User.Identity.Name);

			model.Article.AppUserId = userValue.Id;
			model.Article.CreatedDate = DateTime.Now;

			CreateArticleValidator validationRules = new CreateArticleValidator();
			ValidationResult result = validationRules.Validate(model.Article);

			if (result.IsValid)
			{
				_articleService.TInsert(model.Article);				
				return RedirectToAction("MyArticleList");
			}
			else
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
			}

			// Kategori listesini yeniden doldur
			var categoryList = _categoryService.TGetAll();
			model.CategoryList = categoryList.Select(x => new SelectListItem
			{
				Text = x.CategoryName,
				Value = x.CategoryId.ToString()
			}).ToList();
			model.CategoryList.Insert(0, new SelectListItem { Text = "Kategori seçiniz", Value = "" });

			
			return View(model); 
		}
	}
}
