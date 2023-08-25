using MediatR;
using News.WebUI.Application.Contents_Module;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace News.WebUI.Application.User_Module
{
    public class GetUserByIdQuerry:IRequest<User>
    {
        public int UserID { get; set; }
        public class GetUserByIdQuerryHandler : IRequestHandler<GetUserByIdQuerry, User>
        {
            private readonly Context _context;

            public GetUserByIdQuerryHandler(Context context)
            {
                _context = context;
            }

            public async Task<User> Handle(GetUserByIdQuerry request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.Where(x => x.UserID == request.UserID).FirstOrDefaultAsync();
                if (user == null)
                {
                    return null;
                }
                return user;
            }
        }       
    }
}
