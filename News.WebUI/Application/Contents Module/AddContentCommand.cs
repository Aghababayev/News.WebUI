using MediatR;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using System.Threading;
using System.Threading.Tasks;

namespace News.WebUI.Application.Contents_Module
{
    public class AddContentCommand:IRequest<int>
    {
        public int ContentID { get; set; }
        public string ContentName { get; set; }

        public class AddContentCommandHandler : IRequestHandler<AddContentCommand, int>
        {
            private readonly Context _context;

            public AddContentCommandHandler(Context context)
            {
                _context = context;
            }

            public async Task<int> Handle(AddContentCommand command, CancellationToken cancellationToken)
            {
                var content= new Content
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
