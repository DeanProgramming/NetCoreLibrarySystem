using DeanHLibrarySite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DeanHLibrarySite.Pages.StaffControls
{
    public class BookHistoryCheckModel : PageModel
    { 
        public IList<BookReservations> BookReservations { get; set; } = default!;
        public List<string> UserNames { get; set; } = new List<string>();

        private readonly DeanHLibrarySite.Data.DeanHLibrarySiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookHistoryCheckModel(DeanHLibrarySite.Data.DeanHLibrarySiteContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task OnGetAsync(int bookID)
        {
            IsAdmin = false;

            if (User != null)
            {
                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                    // Check if the user is not null
                    if (user != null)
                    {
                        IsAdmin = user.IsAdmin;
                    }
                }
            }

            var userBookings = from m in _context.BookReservations
                               select m;
             
            var itemsTakenOutByUserIds = userBookings
                .Where(i => i.BookID == bookID);

            itemsTakenOutByUserIds = itemsTakenOutByUserIds.OrderBy(x => x.ReturnDate);

            BookReservations = await itemsTakenOutByUserIds.ToListAsync();

            UserNames.Clear();

            for (int i = 0; i < BookReservations.Count; i++)
            {
                ApplicationUser? applicationUser = _userManager.Users.FirstOrDefault(b => b.Id == BookReservations[i].UserID);

                if (applicationUser != null)
                {
                    UserNames.Add(applicationUser.Name);
                }
            } 
        }

        [BindProperty(SupportsGet = true)]
        public bool IsAdmin { get; set; } = true;
    }
}
