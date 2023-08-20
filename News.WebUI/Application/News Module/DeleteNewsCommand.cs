using MediatR;
using News.WebUI.DataAccess.Concrete;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace News.WebUI.Application.News_Module
{
    public class DeleteNewsCommand:IRequest<int>
    {
        public int InformationID { get; set; }

        public class DeleteVisitorCommandHandler : IRequestHandler<DeleteNewsCommand, int>
        {
            private readonly Context _context;

            public DeleteVisitorCommandHandler(Context context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteNewsCommand command, CancellationToken cancellationToken)
            {
                var info = _context.Informations.Where(x => x.InformationID == command.InformationID).FirstOrDefault();
                if (info == null)
                {
                    return default(int);
                }
                _context.Informations.Remove(info);
                await _context.SaveChangesAsync();
                return info.InformationID;
            }
        }
    }
}
