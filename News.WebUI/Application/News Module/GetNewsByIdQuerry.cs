using MediatR;
using Microsoft.EntityFrameworkCore;
using News.WebUI.DataAccess.Concrete;
using News.WebUI.Entities.Concrete;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace News.WebUI.Application.News_Module
{
    public class GetNewsByIdQuerry:IRequest<Information>
    {
        public int InformationID { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public bool IsValid { get; set; }
        public int ContentID { get; set; }
        public string PictureURL { get; set; }
        public int SelectedContentID { get; set; }

        public class GetNewsByIdQuerryHandler : IRequestHandler<GetNewsByIdQuerry, Information>
        {
            private readonly Context _context;

            public GetNewsByIdQuerryHandler(Context context)
            {
                _context = context;
            }

            public async Task<Information> Handle(GetNewsByIdQuerry request, CancellationToken cancellationToken)
            {
                var info= await _context.Informations.Where(x=>x.InformationID== request.InformationID).FirstOrDefaultAsync();
                if (info==null)
                {
                    return null;
                }
                return info;
            }
        }
    } 
}
