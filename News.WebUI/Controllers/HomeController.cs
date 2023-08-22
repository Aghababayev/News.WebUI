using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News.WebUI.Application.Reader_Module;
using News.WebUI.Application.Visitor_Module;
using News.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace News.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
   
        public HomeController(IMediator mediator)
        {
            _mediator = mediator;     
        }

        public  async Task<IActionResult> Index()
        {
            var values = await _mediator.Send(new ListMainNewsQuerry());
            return View(values);
        }

        public async Task<IActionResult> GetByInformatioId(int id)
        {
            var value = await _mediator.Send(new GetByInformatioIdQuerry { InformationID = id });
            return View(value);
        }
        public async Task<IActionResult> GetValidContents()
        {
            var values = await _mediator.Send(new GetContentIdByActiveNewsQuerry());
            return null;
        }
    }
}
