using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News.WebUI.Application.Contents_Module;
using System.Threading.Tasks;

namespace News.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddContentCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return RedirectToAction(nameof(Index), nameof(Content), new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var content = await _mediator.Send(new GetContentByIdQuerry { ContentID = id });
            return View(content);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateContentCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index), nameof(Content), new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View();
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteContentCommand { ContentID = id });
            return RedirectToAction(nameof(Index), nameof(Content), new { area = "Admin" });
        }
    }
}
