using DeanHLibrarySite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace DeanHLibrarySite.Pages.StaffControls
{
    public class ReturnBookingIndexModel : PageModel
    {
        public IList<BookTable> BookTable { get; set; } = default!;
        public IList<BookReservations> BookReservations { get; set; } = default!;
        public List<string> UserNames { get; set; } = new List<string>();

        private readonly DeanHLibrarySite.Data.DeanHLibrarySiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReturnBookingIndexModel (DeanHLibrarySite.Data.DeanHLibrarySiteContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task OnGetAsync(int? pageNumber, string? title, string? author, string? genre, DateTime? publicationYear, BookTable.BookType? bookType, string? userID, string? userName)
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

            
            var bookList = from m in _context.BookTable
                           select m;

            IQueryable<string> genreQuery = from m in _context.BookTable
                                            orderby m.Genre
                                            select m.Genre;

            IQueryable<Models.BookTable.BookType> bookTypeQuery = (from m in _context.BookTable
                                                                   orderby m.Type
                                                                   select m.Type);

            var userBookings = from m in _context.BookReservations
                               select m;

            Title = title;
            Author = author;
            Genre = genre;
            PublicationYear = publicationYear;
            UserID = userID;
            UserName = userName;

            if (bookType != null)
            {
                SelectedBookTypes = bookType;
            }

            userBookings = userBookings.Where(s => s.Booked);
            userBookings = userBookings.OrderBy(s => s.BookID);
            var bookingBookIDs = userBookings.Select(s => s.BookID);
            bookList = bookList.Where(s => bookingBookIDs.Contains(s.Id));

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

            if (!string.IsNullOrEmpty(UserID))
            {
                var foundUser = await _userManager.FindByIdAsync(UserID);
                FoundUser = foundUser != null;

                if (foundUser != null)
                {
                    var latestBookings = userBookings
                        .GroupBy(i => i.BookID)
                        .Select(group => group.OrderByDescending(i => i.ReturnDate).FirstOrDefault())
                        .ToList(); // Materialize the query to avoid issues

                    var bookIdsTakenOutByUser = latestBookings
                        .Where(i => i.UserID == foundUser.Id)
                        .Select(i => i.BookID)
                        .ToList(); // Extract BookIDs from latestBookings

                    userBookings = userBookings.Where(s => s.UserID == foundUser.Id);
                    bookList = bookList.Where(s => bookIdsTakenOutByUser.Contains(s.Id));
                } 
            }

            if (!string.IsNullOrEmpty(UserName))
            {
                _userManager.FindByNameAsync(UserName).Wait();
                ApplicationUser foundUser = await _userManager.FindByNameAsync(UserName);
                FoundUser = foundUser != null;

                if (foundUser != null)
                {
                    var latestBookings = userBookings
                        .GroupBy(i => i.BookID)
                        .Select(group => group.OrderByDescending(i => i.ReturnDate).FirstOrDefault())
                        .ToList(); // Materialize the query to avoid issues

                    var bookIdsTakenOutByUser = latestBookings
                        .Where(i => i.UserID == foundUser.Id)
                        .Select(i => i.BookID)
                        .ToList(); // Extract BookIDs from latestBookings

                    userBookings = userBookings.Where(s => s.UserID == foundUser.Id);
                    bookList = bookList.Where(s => bookIdsTakenOutByUser.Contains(s.Id));
                }
            }

            bookList = bookList.Where(s => bookingBookIDs.Contains(s.Id)); 

            int pageSize = 5;
            AmountOfPages = (int)Math.Ceiling((float)bookList.Count() / (float)pageSize);

            int itemsToSkip = ((pageNumber ?? 1) - 1) * pageSize;

            bookList = bookList.Skip(itemsToSkip).Take(pageSize);

            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            BookTypes = new SelectList(await bookTypeQuery.Distinct().ToListAsync());
            BookTable = await bookList.ToListAsync();
            BookReservations = await userBookings.ToListAsync();

            UserNames.Clear();

            for (int e = itemsToSkip; e < BookReservations.Count; e++)
            {
                ApplicationUser? applicationUser = _userManager.Users.FirstOrDefault(b => b.Id == BookReservations[e].UserID);

                if (applicationUser != null)
                {
                    UserNames.Add(applicationUser.Name);
                }
            }
        }

        [BindProperty(SupportsGet = true)]
        public string? Title { get; set; }

        public SelectList? BookTypes { get; set; }
        [BindProperty(SupportsGet = true)]
        public Models.BookTable.BookType? SelectedBookTypes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Author { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? UserID { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? UserName { get; set; }

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
        [BindProperty(SupportsGet = true)]
        public bool FoundUser { get; set; } = true;
        [BindProperty(SupportsGet = true)]
        public bool IsAdmin { get; set; } = true;
    }
}
