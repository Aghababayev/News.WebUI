using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using News.WebUI.DataAccess.Concrete;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace News.WebUI.Application.Contents_Module
{
    public class UpdateContentCommand : IRequest<int>
    {
        public int ContentID { get; set; }
        public string ContentName { get; set; }
        public class UpdateContentCommandHandler : IRequestHandler<UpdateContentCommand, int>
        {
            private readonly Context _context;
            private readonly IValidator<UpdateContentCommand> _validator;

            public UpdateContentCommandHandler(Context context, IValidator<UpdateContentCommand> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<int> Handle(UpdateContentCommand command, CancellationToken cancellationToken)
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
                    var content = await _context.Contents.Where(x => x.ContentID == command.ContentID).FirstOrDefaultAsync();
                    if (content == null)
                    {
                        return default;
                    }
                    content.ContentName = command.ContentName;
                    await _context.SaveChangesAsync();
                    return content.ContentID;
                }

            }
        }
    }
}
