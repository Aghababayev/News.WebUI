using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using News.WebUI.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using Information = News.WebUI.Entities.Concrete.Information;

namespace News.WebUI.Application.News_Module
{
    public class AddNewsCommand:IRequest<int>
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public bool IsValid { get; set; } 
        public int ContentID { get; set; }
        public int SelectedContentID { get; set; }


        public class AddNewsCommandHandler : IRequestHandler<AddNewsCommand, int>
        {
            private readonly Context _context;

            public AddNewsCommandHandler(Context context)
            {
                _context = context;
            }

            public async Task<int> Handle(AddNewsCommand command, CancellationToken cancellationToken)
            {
                var news = new Information
                {
                    Body = command.Body,
                    Created = DateTime.Now,
                    Header = command.Header,
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
