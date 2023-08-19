using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using News.WebUI.Application.News_Module;
using News.WebUI.ViewModels;
using System.Threading.Tasks;

namespace News.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IMediator _mediator; 
        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            var values = await _mediator.Send(new ListNewsQuerry());
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var contents = await _mediator.Send(new GetContentTypeQuerry());
            var model = new InfoContentVM
            {
                Contents = contents
            };
            return View(model);

        }
        
        
       
    }
    
}
