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

namespace DeanHLibrarySite.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly DeanHLibrarySite.Data.DeanHLibrarySiteContext _context;

        public IndexModel(DeanHLibrarySite.Data.DeanHLibrarySiteContext context)
        {
            _context = context;
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

            //SelectedBookTypes = bookType;

            // Preserve filter values in the query string
            var queryString = new StringBuilder("?");

            if (!string.IsNullOrEmpty(Title))
            {
                queryString.Append($"Title={Title}&");
            }

            if (!string.IsNullOrEmpty(Author))
            {
                queryString.Append($"Author={Author}&");
            }

            if (!string.IsNullOrEmpty(Genre))
            {
                queryString.Append($"Genre={Genre}&");
            }

            if (PublicationYear != null)
            {
                queryString.Append($"PublicationYear={PublicationYear}&");
            }

            if (SelectedBookTypes != null)
            {
                queryString.Append($"SelectedBookTypes={SelectedBookTypes}&");
            }

            // Remove trailing '&' character
            queryString.Length--;

            // Generate URLs with query string parameters
            PreviousPageUrl = Url.Page("./Index", new { pageNumber = pageNumber - 1, title, author, genre, publicationYear, bookType = SelectedBookTypes }) + queryString.ToString();
            NextPageUrl = Url.Page("./Index", new { pageNumber = pageNumber + 1, title, author, genre, publicationYear, bookType = SelectedBookTypes }) + queryString.ToString();


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
        }


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
