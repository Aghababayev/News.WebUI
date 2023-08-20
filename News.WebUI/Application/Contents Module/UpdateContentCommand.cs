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

            public UpdateContentCommandHandler(Context context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateContentCommand command, CancellationToken cancellationToken)
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
