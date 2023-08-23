using MediatR;
using Microsoft.EntityFrameworkCore;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace News.WebUI.Application.Visitor_Module
{
    public class GetNewsByContetIdQuerry : IRequest<List<Information>>
    {
        public int ContentID { get; set; }
        public class GetNewsByContetIdQuerryHandler : IRequestHandler<GetNewsByContetIdQuerry, List<Information>>
        {
            private readonly Context _context;

            public GetNewsByContetIdQuerryHandler(Context context)
            {
                _context = context;
            }

            public async Task<List<Information>> Handle(GetNewsByContetIdQuerry request, CancellationToken cancellationToken)
            {
                var infos = await _context.Informations.Include(x=>x.Content).Where(x => x.ContentID == request.ContentID && x.IsValid == true).ToListAsync();
                return infos;
            }
        }
    }
}
