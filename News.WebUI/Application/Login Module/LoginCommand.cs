using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.WebUI.Areas.Admin.Controllers;
using News.WebUI.DataAccess.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace News.WebUI.Application.Login_Module
{
    public class LoginCommand : IRequest<IActionResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, IActionResult>
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginCommandHandler(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var value = _context.Users.FirstOrDefault(x => x.UserName == command.UserName && x.Password == command.Password);
            if (value != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, command.UserName),
                    new Claim(ClaimTypes.Role, value.Role),
                };
                var useridentity = new ClaimsIdentity(claims, "Login");

                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await _httpContextAccessor.HttpContext.SignInAsync(principal);

                return new RedirectToActionResult(nameof(DashboardController.Index), "Dashboard", new { area = "Admin" });
            }
            return new UnauthorizedResult();
        }
    }
}
