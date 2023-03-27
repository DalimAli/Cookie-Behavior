using Cookie_Behavior.Models;
using Cookie_Behavior.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cookie_Behavior.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoggedInUserService _loggedInUserService;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, ILoggedInUserService loggedInUserService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _loggedInUserService = loggedInUserService;
        }

        public IActionResult Index()
        {
            var name = HttpContext?.User?.Identity?.Name;
            var IsAuthenticated = HttpContext?.User?.Identity?.IsAuthenticated;
            var userId = _httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated;
            var isA = _loggedInUserService.IsAuthenticated;
            var r = User?.Identity?.IsAuthenticated;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}