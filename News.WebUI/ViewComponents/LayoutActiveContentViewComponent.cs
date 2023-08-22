using MediatR;
using Microsoft.AspNetCore.Mvc;
using News.WebUI.Application.Reader_Module;
using System.Threading.Tasks;

namespace News.WebUI.ViewComponents
{
    public class LayoutActiveContentViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;
        public LayoutActiveContentViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _mediator.Send(new GetContentIdByActiveNewsQuerry());
            return View(values);
        }

    }
}
