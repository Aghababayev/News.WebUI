using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News.WebUI.Application.Contents_Module;
using System.Threading.Tasks;

namespace News.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ContentController : Controller
    {
        public readonly IMediator _mediator;

        public ContentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var contents = await _mediator.Send(new ListContentQuerry());
            return View(contents);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddContentCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index), nameof(Content), new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var contents = await _mediator.Send(new GetContentByIdQuerry { ContentID=id});
            return View(contents);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateContentCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index), nameof(Content), new { area = "Admin" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteContentCommand { ContentID = id });
            return RedirectToAction(nameof(Index), nameof(Content), new { area = "Admin" });
        }
    }
}
