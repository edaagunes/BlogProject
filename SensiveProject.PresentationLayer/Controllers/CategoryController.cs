using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SensiveProject.BusinessLayer.Abstract;
using SensiveProject.BusinessLayer.ValidationRules.CategoryValidation;
using SensiveProject.EntityLayer.Concrete;

namespace SensiveProject.PresentationLayer.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public IActionResult CategoryList()
		{
			var values = _categoryService.TGetAll();
			return View(values);
		}

		public IActionResult CreateCategory()
		{
			
			return View();
		}

		[HttpPost]
		public IActionResult CreateCategory(Category category)
		{
			ModelState.Clear();
			CreateCategoryValidator validationRules= new CreateCategoryValidator();
			ValidationResult result =validationRules.Validate(category);

			if (result.IsValid)
			{
				_categoryService.TInsert(category);
				return RedirectToAction("CategoryList");
			}
			else
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
				return View();
			}
		}

		public IActionResult DeleteCategory(int id)
		{
			_categoryService.TDelete(id);
			return RedirectToAction("CategoryList");
		}

		[HttpGet]
		public IActionResult UpdateCategory(int id)
		{
			var value=_categoryService.TGetById(id);
			return View(value);
		}

		[HttpPost]
		public IActionResult UpdateCategory(Category category)
		{
			_categoryService.TUpdate(category);
			return RedirectToAction("CategoryList");
		}
	}
}
