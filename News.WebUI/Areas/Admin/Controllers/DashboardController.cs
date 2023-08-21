﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News.WebUI.Application.News_Module;
using News.WebUI.ViewModels;
using System;
using System.Data;
using System.Threading.Tasks;

namespace News.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Accessor,Moderator")]
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

        [HttpPost]
        public async Task<IActionResult> Add(AddNewsCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index), "Dashboard", new { area = "Admin" });
        } 
        public async Task<IActionResult> Update(int id)
        {
            var news = await _mediator.Send(new GetNewsByIdQuerry { InformationID = id });
            var contents = await _mediator.Send(new GetContentTypeQuerry());
            var model = new InfoContentVM
            {
                Contents = contents,
                Body=news.Body,
                InformationID=id,
                ContentID=news.ContentID,
                IsValid=news.IsValid,
                Header=news.Header,
            };         
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>Update(UpdateNewsCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index), "Dashboard", new { area = "Admin" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteNewsCommand { InformationID=id});
            return RedirectToAction(nameof(Index), "Dashboard", new { area = "Admin" });
        }


    }
    
}
