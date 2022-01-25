using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Consult_Company.Data;
using Consulting_Company.Model;

namespace Consult_Company.Pages.Uppdrags
{
    public class DetailsModel : PageModel
    {
        private readonly Consult_Company.Data.ApplicationDbContext _context;

        public DetailsModel(Consult_Company.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
