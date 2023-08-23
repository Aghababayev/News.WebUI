using FluentValidation;
using News.WebUI.Application.Contents_Module;
using News.WebUI.DataAccess.Concrete;
using System.Linq;

namespace News.WebUI.Validators.ContentValidator
{
    public class ContentAddValidator : AbstractValidator<AddContentCommand>
    {
        private readonly Context _context;
        public ContentAddValidator(Context context)
        {
            _context = context;
            RuleFor(x => x.ContentName)
                .NotNull().WithMessage("Please insert brand name")
                .MinimumLength(2).WithMessage("Minimum 2 characters")
                .MaximumLength(20).WithMessage("Maximum 20 characters")
                .Must(UniqueName).WithMessage("Content already exists");
        }
        private bool UniqueName(string contentName)
        {
            if (contentName == null)
            {
                return false;
            }
            {
                return !_context.Contents.Any(c => c.ContentName.ToLower() == contentName.ToLower());
            }
        }
    }
}
