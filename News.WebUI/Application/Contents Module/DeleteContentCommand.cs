using MediatR;
using Microsoft.EntityFrameworkCore;
using News.WebUI.DataAccess.Concrete;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace News.WebUI.Application.Contents_Module
{
    public class DeleteContentCommand:IRequest<int>
    {
        public int ContentID { get; set; }
        public class DeleteContentCommandHandler : IRequestHandler<DeleteContentCommand, int>
        {
            private readonly Context _context;

            public DeleteContentCommandHandler(Context context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteContentCommand command, CancellationToken cancellationToken)
            {
                var content = await _context.Contents.Where(c => c.ContentID == command.ContentID).FirstOrDefaultAsync();
                if (content == null)
                {
                    return default(int);
                }
                _context.Contents.Remove(content);
                await _context.SaveChangesAsync();
                return content.ContentID;
            }
        }
    }
}
