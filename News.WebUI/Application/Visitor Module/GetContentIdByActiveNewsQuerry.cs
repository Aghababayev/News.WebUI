using MediatR;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.EntityFrameworkCore;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace News.WebUI.Application.Reader_Module
{
    public class GetContentIdByActiveNewsQuerry : IRequest<List<Content>>
    {
        public int ContentID { get; set; }

        public class GetContentIdByActiveNewsQuerryHandler : IRequestHandler<GetContentIdByActiveNewsQuerry, List<Content>>
        {
            private readonly Context _context;

            public GetContentIdByActiveNewsQuerryHandler(Context context)
            {
                _context = context;
            }

            public async Task<List<Content>> Handle(GetContentIdByActiveNewsQuerry request, CancellationToken cancellationToken)
            {
                var value = await _context.Informations.Where(info => info.IsValid)
                                                       .Include(info => info.Content)
                                                       .Select(info => new Content
                                                       {
                                                       ContentID = info.Content.ContentID,
                                                       ContentName = info.Content.ContentName
                                                       })
                                                      .ToListAsync(cancellationToken);             
                return value;
            }
        }
    }
}
