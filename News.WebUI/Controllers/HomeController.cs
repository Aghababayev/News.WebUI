using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News.WebUI.Application.Reader_Module;
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
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
   
        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;   
        }

        public  async Task<IActionResult> Index()
        {
            var values = await _mediator.Send(new ListMainNewsQuerry());
            return View(values);
        }

       public async Task<IActionResult> GetByContentId()
        {
            var values = await _mediator.Send(new GetContentIdByActiveNewsQuerry());
            return null;
        }
    }
}
