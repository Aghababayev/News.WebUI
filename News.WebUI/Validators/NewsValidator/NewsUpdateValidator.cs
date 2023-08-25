using FluentValidation;
using News.WebUI.Application.News_Module;

namespace News.WebUI.Validators.NewsValidator
{
    public class NewsUpdateValidator : AbstractValidator<UpdateNewsCommand>
    {
        public NewsUpdateValidator()
        {
            RuleFor(x => x.Header)
                .NotNull().WithMessage("Please insert header")
                    .MinimumLength(30).WithMessage("Minimum 40 characters")
                .MaximumLength(100).WithMessage("Maximum 100 characters");

            RuleFor(x => x.Body)
                .NotNull().WithMessage("Please insert Body")
                .MinimumLength(50).WithMessage("Minimum 50 characters");
        }
    }
}
