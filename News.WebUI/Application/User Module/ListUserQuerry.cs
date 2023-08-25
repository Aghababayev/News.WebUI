using MediatR;
using News.WebUI.Application.News_Module;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace News.WebUI.Application.User_Module
{
    public class ListUserQuerry:IRequest<List<User>>
    {
        public class ListUserQuerryHandler : IRequestHandler<ListUserQuerry, List<User>>
        {
            private readonly Context _context;

            public ListUserQuerryHandler(Context context)
            {
                _context = context;
            }
            public async Task<List<User>> Handle(ListUserQuerry request, CancellationToken cancellationToken)
            {
                var values = await _context.Users.ToListAsync();
                return values;
            }
        }
    }
}
