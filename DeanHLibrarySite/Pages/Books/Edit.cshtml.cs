using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeanHLibrarySite.Models;
using System.Text;

namespace DeanHLibrarySite.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly DeanHLibrarySite.Data.DeanHLibrarySiteContext _context;

        public EditModel(DeanHLibrarySite.Data.DeanHLibrarySiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookTable BookTable { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? pageNumber, string? title, string? author, string? genre, DateTime? publicationYear, BookTable.BookType? bookType)
        {
            if (id == null || _context.BookTable == null)
            {
                return NotFound();
            }

            var booktable =  await _context.BookTable.FirstOrDefaultAsync(m => m.Id == id);
            if (booktable == null)
            {
                return NotFound();
            }
            BookTable = booktable;

            ItemId = id;
            Title = title;
            Author = author;
            Genre = genre; 
            PageNumber = pageNumber; 
            PublicationYear = publicationYear;

            if (bookType != null)
            {
                SelectedBookTypes = bookType;
            }


            AvailableBookTypes = Enum.GetValues(typeof(BookTable.BookType))
                .Cast<BookTable.BookType>()
                .Select(bookType => new SelectListItem
                {
                    Value = bookType.ToString(),
                    Text = bookType.ToString()
                })
                .ToList();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD. 
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BookTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookTableExists(BookTable.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Preserve filter values in the query string
            var queryString = new StringBuilder("&");

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

            // Remove the trailing '&' character
            if (queryString.Length > 1)
            {
                queryString.Length--;
            }

            // Redirect back to the "Index" page with the preserved query parameters// Redirect back to the "Index" page with the preserved query parameters
            var redirectUrl = Url.Page("./Index", new { pageNumber = PageNumber }) + queryString.ToString();
            return Redirect(redirectUrl);
        }

        private bool BookTableExists(int id)
        {
          return (_context.BookTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public List<SelectListItem> AvailableBookTypes { get; set; }

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
    }
}
