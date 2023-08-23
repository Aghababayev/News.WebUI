using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using News.WebUI.Application.Contents_Module;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using News.WebUI.ViewModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Information = News.WebUI.Entities.Concrete.Information;

namespace News.WebUI.Application.News_Module
{
    public class AddNewsCommand : IRequest<int>
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public bool IsValid { get; set; }
        public int ContentID { get; set; }
        public int SelectedContentID { get; set; }
        public string PictureUrl { get; set; }


        public class AddNewsCommandHandler : IRequestHandler<AddNewsCommand, int>
        {
            private readonly Context _context;
            private readonly IValidator<AddNewsCommand> _validator;
            public AddNewsCommandHandler(Context context, IValidator<AddNewsCommand> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<int> Handle(AddNewsCommand command, CancellationToken cancellationToken)
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
                    var news = new Information
                    {
                        Body = command.Body,
                        Created = DateTime.Now,
                        Header = command.Header,
                        PictureURL = command.PictureUrl,
                        IsValid = false,
                        ContentID = command.SelectedContentID

                    };
                    await _context.Informations.AddAsync(news);
                    await _context.SaveChangesAsync(cancellationToken);
                    return news.InformationID;
                }

            }
        }
    }

}
