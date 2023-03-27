using Cookie_Behavior.Services.Interfaces;
using System.Security.Claims;

namespace Cookie_Behavior.Services.Implementation
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;

        }

        private ClaimsPrincipal User => _httpContextAccessor?.HttpContext?.User;

        public long UserId => User != null && User.Identity.IsAuthenticated ? int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) : 0;
        public string UserEmail => User != null && User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.Email) : string.Empty;
        public string MobileNumber => User != null && User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.MobilePhone) : string.Empty;
        public string UserName => User != null && User.Identity.IsAuthenticated ? User.Identity.Name : string.Empty;
        public bool IsAuthenticated => User != null && User.Identity.IsAuthenticated;
        public string RequestOrigin => _httpContextAccessor?.HttpContext.Request.Headers["Origin"].ToString()?.Trim();
        public List<string> Roles => User != null && User.Identity.IsAuthenticated ? User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList() : null;
    }
}
