using FluentValidation;
using News.WebUI.Application.News_Module;

namespace News.WebUI.Validators.NewsValidator
{
    public class NewsAddValidator:AbstractValidator<AddNewsCommand>
    {
        public NewsAddValidator()
        {
            RuleFor(x => x.Header)
           .NotNull().WithMessage("Please insert header")
           .MinimumLength(5).WithMessage("Minimum 5 characters")
           .MaximumLength(100).WithMessage("Maximum 100 characters");
            RuleFor(x => x.Body)
            .NotNull().WithMessage("Please insert Body")
            .MinimumLength(50).WithMessage("Minimum 50 characters");
        }
    }
}
