using MediatR;
using Microsoft.EntityFrameworkCore;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace News.WebUI.Application.Contents_Module
{
    public class GetContentByIdQuerry:IRequest<Content>
    {
        public int ContentID { get; set; }
        public int UserID { get; internal set; }

        public class GetContentByIdQuerryHandler : IRequestHandler<GetContentByIdQuerry, Content>
        {
            private readonly Context _context;

            public GetContentByIdQuerryHandler(Context context)
            {
                _context = context;
            }

            public async Task<Content> Handle(GetContentByIdQuerry request, CancellationToken cancellationToken)
            {
                var content = await _context.Contents.Where(x => x.ContentID == request.ContentID).FirstOrDefaultAsync();
                if (content == null)
                {
                    return null;
                }
                return content;
            }
        }
    }
}
