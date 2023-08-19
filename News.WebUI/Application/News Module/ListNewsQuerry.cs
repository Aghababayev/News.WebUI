using MediatR;
using Microsoft.EntityFrameworkCore;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace News.WebUI.Application.News_Module
{
    public class ListNewsQuerry : IRequest<List<Information>>
    {
        public class ListNewsQuerryHandler : IRequestHandler<ListNewsQuerry, List<Information>>
        {
            private readonly Context _context;

            public ListNewsQuerryHandler(Context context)
            {
                _context = context;
            }

            public async Task<List<Information>> Handle(ListNewsQuerry request, CancellationToken cancellationToken)
            {
                var values = await _context.Informations.Include(x=>x.Content).ToListAsync();
                return values;
            }
        }
    }
}
