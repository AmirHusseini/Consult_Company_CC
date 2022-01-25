using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Consult_Company.Data;
using Consulting_Company.Model;

namespace Consult_Company.Pages.Projekter
{
    public class DeleteModel : PageModel
    {
        private readonly Consult_Company.Data.ApplicationDbContext _context;

        public DeleteModel(Consult_Company.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; }
        public List<Uppdrag> Uppdrags { get; set; }
        public List<ApplicationUser> ApplicationUsers { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects.FirstOrDefaultAsync(m => m.IdProject == id);
            Uppdrags = _context.Uppdrags.ToList();
            ApplicationUsers = _context.Users.ToList();



            foreach (var item2 in Uppdrags)
            {

                if (Project.IdTask == item2.IdTask)
                {
                    Project.Task = new Uppdrag();
                    Project.Task = item2 as Uppdrag;
                }

            }
            foreach (var item3 in ApplicationUsers)
            {
                if (Project.UserId == item3.Id)
                {
                    Project.IdentUser = new ApplicationUser();
                    Project.IdentUser = item3 as ApplicationUser;
                }
            }
            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects.FindAsync(id);

            if (Project != null)
            {
                _context.Projects.Remove(Project);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
