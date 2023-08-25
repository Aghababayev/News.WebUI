using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News.WebUI.Application.Contents_Module;
using News.WebUI.Application.User_Module;
using News.WebUI.Application.Users_Module;
using System.Data;
using System.Threading.Tasks;

namespace News.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var contents = await _mediator.Send(new ListUserQuerry());
            return View(contents);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuerry { UserID = id });
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePasswordCommand command,int id)
        {
            try
            {
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index), nameof(User), new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                var user = await _mediator.Send(new GetUserByIdQuerry { UserID = id });
                return View(user);
            }
        }
    }
}
