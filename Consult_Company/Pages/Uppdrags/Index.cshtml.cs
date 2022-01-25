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
    public class IndexModel : PageModel
    {
        private readonly Consult_Company.Data.ApplicationDbContext _context;

        public IndexModel(Consult_Company.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Uppdrag> Uppdrag { get;set; }

        public async Task OnGetAsync()
        {
            Uppdrag = await _context.Uppdrags.ToListAsync();
        }
    }
}
