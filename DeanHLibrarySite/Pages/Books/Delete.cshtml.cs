using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeanHLibrarySite.Data;
using DeanHLibrarySite.Models;

namespace DeanHLibrarySite.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly DeanHLibrarySite.Data.DeanHLibrarySiteContext _context;

        public DeleteModel(DeanHLibrarySite.Data.DeanHLibrarySiteContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BookTable BookTable { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.BookTable == null)
            {
                return NotFound();
            }
            var booktable = await _context.BookTable.FindAsync(id);

            if (booktable != null)
            {
                BookTable = booktable;
                _context.BookTable.Remove(BookTable);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
