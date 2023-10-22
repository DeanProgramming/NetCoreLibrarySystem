using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DeanHLibrarySite.Data;
using DeanHLibrarySite.Models;

namespace DeanHLibrarySite.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly DeanHLibrarySite.Data.DeanHLibrarySiteContext _context;

        public CreateModel(DeanHLibrarySite.Data.DeanHLibrarySiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
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

        [BindProperty]
        public BookTable BookTable { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.BookTable == null || BookTable == null)
            {
                return Page();
            }

            _context.BookTable.Add(BookTable);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public List<SelectListItem> AvailableBookTypes { get; set; }
    }
}
