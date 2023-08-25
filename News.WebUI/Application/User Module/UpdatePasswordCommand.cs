using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using News.WebUI.DataAccess.Concrete;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace News.WebUI.Application.Users_Module
{
    public class UpdatePasswordCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string Password { get; set; }

       
        public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, int>
        {
            private readonly Context _context;
            private readonly IValidator<UpdatePasswordCommand> _validator;

            public UpdatePasswordCommandHandler(Context context, IValidator<UpdatePasswordCommand> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<int> Handle(UpdatePasswordCommand command, CancellationToken cancellationToken)
            {
                var validationResults = await _validator.ValidateAsync(command);
                if (!validationResults.IsValid)
                {
                    var validationFailures = validationResults.Errors
                         .Select(error => new ValidationFailure(error.PropertyName, error.ErrorMessage))
                         .ToList();
                    throw new ValidationException(validationFailures);
                }
                else
                {
                    var user = await _context.Users.Where(x => x.UserId == command.UserId).FirstOrDefaultAsync();
                    if (user == null)
                    {
                        return default;
                    }
                    user.Password = command.Password;
                    await _context.SaveChangesAsync();
                    return user.UserId;
                }
            }
        }
    }
}
