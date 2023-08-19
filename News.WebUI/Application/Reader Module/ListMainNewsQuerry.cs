using MediatR;
using Microsoft.EntityFrameworkCore;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace News.WebUI.Application.Reader_Module
{
    public class ListMainNewsQuerry:IRequest<List<Information>>
    {
        public class ListMainNewsQuerryHandler : IRequestHandler<ListMainNewsQuerry, List<Information>>
        {
            private readonly Context _context;

            public ListMainNewsQuerryHandler(Context context)
            {
                _context = context;
            }

            public async Task<List<Information>> Handle(ListMainNewsQuerry request, CancellationToken cancellationToken)
            {
               var values= await _context.Informations.Where(v=>v.IsValid).ToListAsync();   
                return values;
            }
        }
    }
}
