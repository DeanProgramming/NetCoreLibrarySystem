using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeanHLibrarySite.Data;
using DeanHLibrarySite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;

namespace DeanHLibrarySite.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly DeanHLibrarySite.Data.DeanHLibrarySiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(DeanHLibrarySite.Data.DeanHLibrarySiteContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public IList<BookTable> BookTable { get;set; } = default!; 


        public async Task OnGetAsync(int? pageNumber, string? title, string? author, string? genre, DateTime? publicationYear, BookTable.BookType? bookType)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.BookTable
                                            orderby m.Genre
                                            select m.Genre;

            IQueryable<Models.BookTable.BookType> bookTypeQuery = (from m in _context.BookTable
                                               orderby m.Type
                                               select m.Type);
             

            var bookList = from m in _context.BookTable
                           select m;

            Title = title;
            Author = author;
            Genre = genre;
            PublicationYear = publicationYear;

            if (bookType != null)
            {
                SelectedBookTypes = bookType;
            }

            if (!string.IsNullOrEmpty(Title))
            {
                bookList = bookList.Where(s => s.Title.Contains(Title));
            }

            if (!string.IsNullOrEmpty(Author))
            {
                bookList = bookList.Where(s => s.Author.Contains(Author));
            }

            if (!string.IsNullOrEmpty(Genre))
            {
                bookList = bookList.Where(x => x.Genre == Genre);
            }

            if (PublicationYear != null)
            {
                bookList = bookList.Where(x => x.PublicationYear >= PublicationYear);
            }

            if (SelectedBookTypes != null)
            {
                bookList = bookList.Where(x => x.Type == SelectedBookTypes);
            }

            int pageSize = 5;
            AmountOfPages = (int)Math.Ceiling((float)bookList.Count() / (float)pageSize);

            int itemsToSkip = ((pageNumber ?? 1) - 1) * pageSize;

            
            
            bookList = bookList.Skip(itemsToSkip).Take(pageSize);


            Genres = new SelectList(await genreQuery.Distinct().ToListAsync()); 
            BookTypes = new SelectList(await bookTypeQuery.Distinct().ToListAsync());
            BookTable = await bookList.ToListAsync();

            var bookIds = BookTable.Select(b => b.Id).ToList();
            var bookedBooks = _context.BookReservations
                .Where(br => bookIds.Contains(br.BookID) && br.Booked)
                .ToList();

            // Initialize the BookAvailability, BookReturnDates, and BookReturnDateExpired dictionaries
            BookAvailability = new Dictionary<int, bool>();
            BookReturnDates = new Dictionary<int, DateTime?>();
            BookReturnDateExpired = new Dictionary<int, bool>();
            foreach (var book in BookTable)
            {
                var isBooked = bookedBooks.Any(br => br.BookID == book.Id);
                BookAvailability[book.Id] = !isBooked;
                if (isBooked)
                {
                    var returnDate = bookedBooks.FirstOrDefault(br => br.BookID == book.Id)?.ReturnDate;
                    BookReturnDates[book.Id] = returnDate;
                    BookReturnDateExpired[book.Id] = returnDate < DateTime.Today;
                }
            }

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
        public Dictionary<int, bool> BookAvailability { get; set; }
        public Dictionary<int, DateTime?> BookReturnDates { get; set; }
        public Dictionary<int, bool> BookReturnDateExpired { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Title { get; set; }

        public SelectList? BookTypes { get; set; }
        [BindProperty(SupportsGet = true)]
        public Models.BookTable.BookType? SelectedBookTypes { get; set; } 

        [BindProperty(SupportsGet = true)]
        public string? Author { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Genre { get; set; } 

        [BindProperty(SupportsGet = true)]
        public DateTime? PublicationYear { get; set; } 

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int AmountOfPages { get; private set; }


        [BindProperty(SupportsGet = true)]
        public string PreviousPageUrl { get; private set; }


        [BindProperty(SupportsGet = true)]
        public string NextPageUrl { get; private set; }
    }
}
