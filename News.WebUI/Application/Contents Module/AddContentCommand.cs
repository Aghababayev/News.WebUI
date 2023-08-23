using FluentValidation;
using FluentValidation.Results;
using MediatR;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace News.WebUI.Application.Contents_Module
{
    public class AddContentCommand : IRequest<int>
    {
        public int ContentID { get; set; }
        public string ContentName { get; set; }

        public class AddContentCommandHandler : IRequestHandler<AddContentCommand, int>
        {
            private readonly Context _context;
            private readonly IValidator<AddContentCommand> _validator;

            public AddContentCommandHandler(Context context, IValidator<AddContentCommand> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<int> Handle(AddContentCommand command, CancellationToken cancellationToken)
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
                    var content = new Content
                    {
                        ContentID = command.ContentID,
                        ContentName = command.ContentName
                    };
                    await _context.AddAsync(content);
                    await _context.SaveChangesAsync();
                    return content.ContentID;
                }
            }
        }
    }
}
