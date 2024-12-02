using FluentValidation;
using SensiveProject.EntityLayer.Concrete;

public class CreateArticleValidator : AbstractValidator<Article>
{
	public CreateArticleValidator()
	{
		RuleFor(x => x.Title).NotEmpty().WithMessage("{PropertyName} boş geçilemez");
		RuleFor(x => x.Description).NotEmpty().WithMessage("{PropertyName} içeriği boş geçilemez");
		RuleFor(x => x.CategoryId).NotEmpty().WithMessage("{PropertyName} seçilmelidir.");
		RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("{PropertyName} seçilmelidir.");
		RuleFor(x => x.CoverImageUrl).NotEmpty().WithMessage("Lütfen yazınıza bir görsel ekleyin.");
		RuleFor(x => x.Title).MinimumLength(3).WithMessage("{PropertyName} için en az 3 karakter veri girişi yapınız");
		RuleFor(x => x.Description).MinimumLength(20).WithMessage("Lütfen en az 20 karakterlik içerik girişi yapınız");
		RuleFor(x => x.Title).MaximumLength(100).WithMessage("{PropertyName} için en fazla 100 karakterlik başlık girişi yapınız");
		RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Lütfen en fazla 1000 karakterlik yazı girişi yapınız");
	}
}
