using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeanHLibrarySite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        { 
            string userLogin = await GetLoggedInUserIdAsync();

            LoggedIn = userLogin != null;
            IsAdmin = await GetLoggedInUserAdminPermissionsAsync();
        }

        private async Task<string> GetLoggedInUserIdAsync()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                // Check if the user is not null
                if (user != null)
                {
                    return user.Id;
                }
            }

            return null;
        }
        private async Task<bool> GetLoggedInUserAdminPermissionsAsync()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                // Check if the user is not null
                if (user != null)
                {
                    return user.IsAdmin;
                }
            }

            return false;
        }


        public bool LoggedIn { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}
