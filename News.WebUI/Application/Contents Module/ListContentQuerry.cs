using MediatR;
using News.WebUI.Application.News_Module;
using News.WebUI.DataAccess.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using News.WebUI.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace News.WebUI.Application.Contents_Module
{
    public class ListContentQuerry : IRequest<List<Content>>
    {       
        public class ListContentQuerryHandler : IRequestHandler<ListContentQuerry, List<Content>>
        {
            private readonly Context _context;

            public ListContentQuerryHandler(Context context)
            {
                _context = context;
            }
            public async Task<List<Content>> Handle(ListContentQuerry request, CancellationToken cancellationToken)
            {
                var values = await _context.Contents.ToListAsync();
                return values;
            }
        }
    }
}
