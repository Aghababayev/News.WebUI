using MediatR;
using News.WebUI.DataAccess.Concrete;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

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
        public int SelectedContentID { get; set; }
        public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, int>
        {
            private readonly Context _context;

            public UpdateNewsCommandHandler(Context context)
            {
                _context = context;
            }

            [HttpPost]
            public async Task<int> Handle(UpdateNewsCommand command, CancellationToken cancellationToken)
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
                await _context.SaveChangesAsync();
                return information.InformationID;
            }
        }
    }
}
