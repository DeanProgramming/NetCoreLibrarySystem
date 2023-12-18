using DeanHLibrarySite.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DeanHLibrarySite.Pages.StaffControls
{
    public class ReturnItemDetailsModel : PageModel
    {
        private readonly DeanHLibrarySite.Data.DeanHLibrarySiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReturnItemDetailsModel(DeanHLibrarySite.Data.DeanHLibrarySiteContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
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


            if (bookID == null || bookID == 0)
            {
                return;
            }

            BookID = bookID;

            var bookDetails = _context.BookTable.FirstOrDefault(b => b.Id == bookID);

            var reservationDetails = _context.BookReservations.FirstOrDefault(r => r.BookID == bookID);

            var reservationName = _userManager.Users.FirstOrDefault(b => b.Id == reservationDetails.UserID);

            BookName = bookDetails.Title;
            BookedUserName = reservationName.Name;
            BookedUserID = reservationDetails.UserID;
            BookHandInDate = reservationDetails.ReturnDate;
            CurrentDate = DateTime.Now; 
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var reservations = await _context.BookReservations
                .FirstOrDefaultAsync(x => x.BookID == BookID && x.Booked);

            if (reservations != null)
            {
                reservations.Booked = false;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ReturnBookingIndex");
        }



        [BindProperty(SupportsGet = true)]
        public bool IsAdmin { get; set; } = true;

        [BindProperty(SupportsGet = true)]
        public int BookID { get; set; } = -1;

        [BindProperty(SupportsGet = true)]
        public string BookName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string BookedUserName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string BookedUserID { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime BookHandInDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime CurrentDate { get; set; }
    }
}
