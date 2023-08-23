using MediatR;
using News.WebUI.DataAccess.Concrete;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;

namespace News.WebUI.Application.News_Module
{
    public class UpdateNewsCommand : IRequest<int>
    {
        public int InformationID { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public bool IsValid { get; set; }
        public int ContentID { get; set; }
        public string PictureUrl { get; set; }
        public int SelectedContentID { get; set; }
        public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, int>
        {
            private readonly Context _context;
            private readonly IValidator<UpdateNewsCommand> _validator;
            public UpdateNewsCommandHandler(Context context, IValidator<UpdateNewsCommand> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<int> Handle(UpdateNewsCommand command, CancellationToken cancellationToken)
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
                    var information = await _context.Informations.Where(x => x.InformationID == command.InformationID).FirstOrDefaultAsync();
                    if (information == null)
                    {
                        return default;
                    }
                    information.Header = command.Header;
                    information.Body = command.Body;
                    information.ContentID = command.ContentID;
                    information.IsValid = command.IsValid;
                    information.PictureURL = command.PictureUrl;
                    await _context.SaveChangesAsync();
                    return information.InformationID;
                }
              
            }
        }
    }
}
