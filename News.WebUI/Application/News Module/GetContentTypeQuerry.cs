using MediatR;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace News.WebUI.Application.News_Module
{
    public class GetContentTypeQuerry:IRequest<List<Content>>
    {
        public class GetContentTypeQuerryHandler : IRequestHandler<GetContentTypeQuerry, List<Content>>
        {
            private readonly Context _context;

            public GetContentTypeQuerryHandler(Context context)
            {
                _context = context;
            }

            public async Task<List<Content>> Handle(GetContentTypeQuerry request, CancellationToken cancellationToken)
            {
                var values = await _context.Contents.ToListAsync();
                return values;
            }
        }
    }
}
