using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Consult_Company.Data;
using Consulting_Company.Model;

namespace Consult_Company.Pages.Uppdrags
{
    public class CreateModel : PageModel
    {
        private readonly Consult_Company.Data.ApplicationDbContext _context;

        public CreateModel(Consult_Company.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Uppdrag Uppdrag { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Uppdrags.Add(Uppdrag);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
