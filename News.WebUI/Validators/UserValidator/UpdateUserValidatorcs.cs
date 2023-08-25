using FluentValidation;
using News.WebUI.Application.Users_Module;
using News.WebUI.Entities.Concrete;
using System.Text.RegularExpressions;

namespace News.WebUI.Validators.UserValidator
{
    public class UpdateUserValidatorcs : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdateUserValidatorcs()
        {
            RuleFor(x => x.Password)
             .NotEmpty().WithMessage("Please insert a password")
             .MinimumLength(5).WithMessage("Minimum 5 characters")
             .MaximumLength(20).WithMessage("Maximum 20 characters")
             .Must(BeValidPassword).WithMessage("Password must contain special characters");
        }

        private bool BeValidPassword(string password)
        {
            return Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]");
        }

    }

}
