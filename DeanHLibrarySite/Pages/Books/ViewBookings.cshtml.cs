using DeanHLibrarySite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DeanHLibrarySite.Pages.Books
{
    public class ViewBookingsModel : PageModel
    { 
        public IList<BookTable> BookTable { get; set; } = default!;
        public IList<BookReservations> BookReservations { get; set; } = default!;

        private readonly DeanHLibrarySite.Data.DeanHLibrarySiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public ViewBookingsModel(DeanHLibrarySite.Data.DeanHLibrarySiteContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var bookList = from m in _context.BookTable
                           select m;


            var userBookings = from m in _context.BookReservations
                           select m;

            string userID = await GetLoggedInUserIdAsync();

            userBookings = userBookings.Where(s => s.UserID == userID);

            var bookingBookIDs = userBookings.Select(s => s.BookID);

            bookList = bookList.Where(s => bookingBookIDs.Contains(s.Id));

            BookTable = await bookList.ToListAsync();
            BookReservations = await userBookings.ToListAsync();

            BookReturnDateExpired = new Dictionary<int, bool>();
            int count = 0;
            foreach (var book in BookTable)
            { 
                var returnDate = userBookings.FirstOrDefault(br => br.BookID == book.Id)?.ReturnDate; 
                BookReturnDateExpired[count] = returnDate < DateTime.Today;
                count++;
            }
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


        public Dictionary<int, bool> BookReturnDateExpired { get; set; }
    }
}
