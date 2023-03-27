using Cookie_Behavior.Models.IdentityInfos;
using Cookie_Behavior.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cookie_Behavior.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(ILoggedInUserService loggedInUserService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _loggedInUserService = loggedInUserService;
            _userManager = userManager;
            this._signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            if (_loggedInUserService?.UserId != null && _loggedInUserService?.UserId != 0)
            {
                long userId = (long)_loggedInUserService.UserId;
                var user = await _userManager.FindByIdAsync(userId.ToString()); //Users.FirstOrDefaultAsync(u => u.Id == userId);
                await _userManager.UpdateSecurityStampAsync(user);
            }

            await _signInManager.SignOutAsync();

            return LocalRedirect("/Identity/Account/Login");
        }
    }
}
