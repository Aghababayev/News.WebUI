using MediatR;
using Microsoft.EntityFrameworkCore;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace News.WebUI.Application.Visitor_Module
{
    public class GetByInformatioIdQuerry:IRequest<Information>
    {
        public int InformationID { get; set; }
        public class GetByInformatioIdQuerryHAndler : IRequestHandler<GetByInformatioIdQuerry, Information>
        {private  readonly Context _context;

            public GetByInformatioIdQuerryHAndler(Context context)
            {
                _context = context;
            }

            public async Task<Information> Handle(GetByInformatioIdQuerry request, CancellationToken cancellationToken)
            {
                var info = await _context.Informations.Where(x => x.InformationID == request.InformationID).FirstOrDefaultAsync();
                return info;
            }
        }
    }
}
