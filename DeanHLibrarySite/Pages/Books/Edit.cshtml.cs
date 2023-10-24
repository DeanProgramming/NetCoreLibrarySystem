using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeanHLibrarySite.Data;
using DeanHLibrarySite.Models;

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

        public async Task<IActionResult> OnGetAsync(int? id)
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

            return RedirectToPage("./Index");
        }

        private bool BookTableExists(int id)
        {
          return (_context.BookTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public List<SelectListItem> AvailableBookTypes { get; set; }
    }
}
