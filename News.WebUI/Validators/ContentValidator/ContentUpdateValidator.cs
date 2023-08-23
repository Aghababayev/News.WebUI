using FluentValidation;
using News.WebUI.Application.Contents_Module;
using News.WebUI.DataAccess.Concrete;
using System.Linq;

namespace News.WebUI.Validators
{
    public class UpdateContentCommandValidator : AbstractValidator<UpdateContentCommand>
    {
        private readonly Context _context;
        public UpdateContentCommandValidator(Context context)
        {
            _context = context;

            RuleFor(x => x.ContentName)
                .NotNull().WithMessage("Please insert content name")
                .MinimumLength(2).WithMessage("Minimum 2 characters")
                .MaximumLength(20).WithMessage("Maximum 20 characters")
                .Must(UniqueName).WithMessage("Content already exists");
        }
        private bool UniqueName(UpdateContentCommand command, string contentName)
        {
            if (contentName == null)
            {
                return false;
            }
            return !_context.Contents.Any(c => c.ContentID != command.ContentID && c.ContentName.ToLower() == contentName.ToLower());
        }
    }
}
