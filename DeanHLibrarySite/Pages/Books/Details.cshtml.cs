using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeanHLibrarySite.Data;
using DeanHLibrarySite.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;

namespace DeanHLibrarySite.Pages.Books
{
    public class DetailsModel : PageModel
    {

        private readonly DeanHLibrarySite.Data.DeanHLibrarySiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(DeanHLibrarySite.Data.DeanHLibrarySiteContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public BookTable BookTable { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id, int? pageNumber, string? title, string? author, string? genre, DateTime? publicationYear, BookTable.BookType? bookType)
        {
            if (id == null || _context.BookTable == null)
            {
                return NotFound();
            }

            var booktable = await _context.BookTable.FirstOrDefaultAsync(m => m.Id == id);
            if (booktable == null)
            {
                return NotFound();
            }
            else 
            {
                BookTable = booktable;
            }

            Title = title;
            Author = author;
            Genre = genre;
            PageNumber = pageNumber;
            PublicationYear = publicationYear;

            if (bookType != null)
            {
                SelectedBookTypes = bookType;
            } 

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

            return Page();
        }


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
        public bool IsAdmin { get; set; } = true;
    }
}
