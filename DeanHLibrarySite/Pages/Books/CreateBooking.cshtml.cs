using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DeanHLibrarySite.Data;
using DeanHLibrarySite.Models;
using Humanizer.Localisation;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DeanHLibrarySite.Pages.Books
{
    public class CreateBookingModel : PageModel
    {
        private readonly DeanHLibrarySite.Data.DeanHLibrarySiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateBookingModel(DeanHLibrarySite.Data.DeanHLibrarySiteContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id, int? pageNumber, string? title, string? author, string? genre, DateTime? publicationYear, BookTable.BookType? bookType, DateTime? returnDate, bool confirmedBooking = false)
        {
            if (id == null || _context.BookReservations == null)
            {
                return NotFound();
            }

            var booktable = await _context.BookTable.FirstOrDefaultAsync(m => m.Id == id);
            if (booktable == null)
            {
                return NotFound();
            }

            BookTitle = booktable.Title;
            ItemId = id;
            Title = title;
            Author = author;
            Genre = genre;
            PageNumber = pageNumber;
            PublicationYear = publicationYear;
            ConfirmedBooking = confirmedBooking;
            BookingDateExpiry = returnDate;

            if (bookType != null)
            {
                SelectedBookTypes = bookType;
            }

            return Page();
        }

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        { 
            if (!ModelState.IsValid || _context.BookReservations == null)
            {
                return Page();
            }

            string userId = await GetLoggedInUserIdAsync();

            BookReservations newReservation = new BookReservations
            { 
                Booked = true,
                ReturnDate = DateTime.Now.AddMonths(1),
                BookID = (int)ItemId,
                UserID = userId
            };

            _context.Add(newReservation);
            await _context.SaveChangesAsync();

            // Preserve filter values in the query string
            var queryString = new StringBuilder("?");

            queryString.Append($"id={ItemId}&");
            queryString.Append($"pageNumber={PageNumber}&");

            if (!string.IsNullOrEmpty(Title))
            {
                queryString.Append($"title={Title}&");
            }

            if (!string.IsNullOrEmpty(Author))
            {
                queryString.Append($"author={Author}&");
            }

            if (!string.IsNullOrEmpty(Genre))
            {
                queryString.Append($"genre={Genre}&");
            }

            if (PublicationYear != null)
            {
                queryString.Append($"publicationYear={PublicationYear}&");
            }

            if (SelectedBookTypes != null)
            {
                queryString.Append($"bookType={SelectedBookTypes.ToString()}&");
            }

            queryString.Append($"returnDate={newReservation.ReturnDate}&");
            queryString.Append($"confirmedBooking={true}&");

            // Remove the trailing '&' character
            if (queryString.Length > 1)
            {
                queryString.Length--;
            } 

            var redirectUrl = Url.Page("./CreateBooking", new {}) + queryString.ToString();
            return Redirect(redirectUrl);
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



        [BindProperty(SupportsGet = true)]
        public string? BookTitle { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? ItemId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? PageNumber { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Title { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Author { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Genre { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? PublicationYear { get; set; }
        [BindProperty(SupportsGet = true)]
        public Models.BookTable.BookType? SelectedBookTypes { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool ConfirmedBooking { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? BookingDateExpiry { get; set; }
    }
}
