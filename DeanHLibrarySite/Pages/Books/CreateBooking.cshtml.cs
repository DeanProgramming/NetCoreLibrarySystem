using System;
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

namespace DeanHLibrarySite.Pages.Books
{
    public class CreateBookingModel : PageModel
    {
        private readonly DeanHLibrarySite.Data.DeanHLibrarySiteContext _context;

        public CreateBookingModel(DeanHLibrarySite.Data.DeanHLibrarySiteContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id, int? pageNumber, string? title, string? author, string? genre, DateTime? publicationYear, BookTable.BookType? bookType)
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

            BookReservations newReservation = new BookReservations
            { 
                Booked = true,
                ReturnDate = DateTime.Now.AddMonths(1),
                BookID = (int)ItemId,
                UserID = 4
            };

            _context.Add(newReservation);
            await _context.SaveChangesAsync();

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
    }
}
