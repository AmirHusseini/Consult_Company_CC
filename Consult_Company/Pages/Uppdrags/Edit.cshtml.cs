using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Consult_Company.Data;
using Consulting_Company.Model;

namespace Consult_Company.Pages.Uppdrags
{
    public class EditModel : PageModel
    {
        private readonly Consult_Company.Data.ApplicationDbContext _context;

        public EditModel(Consult_Company.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Uppdrag Uppdrag { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Uppdrag = await _context.Uppdrags.FirstOrDefaultAsync(m => m.IdTask == id);

            if (Uppdrag == null)
            {
                return NotFound();
            }
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

            _context.Attach(Uppdrag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UppdragExists(Uppdrag.IdTask))
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

        private bool UppdragExists(int id)
        {
            return _context.Uppdrags.Any(e => e.IdTask == id);
        }
    }
}
