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

namespace Consult_Company.Pages.Projekter
{
    public class EditModel : PageModel
    {
        private readonly Consult_Company.Data.ApplicationDbContext _context;

        public EditModel(Consult_Company.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; }
        public List<SelectListItem> Options { get; set; }
        public List<SelectListItem> Options2 { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Options = _context.Uppdrags.Select(a =>
                              new SelectListItem
                              {
                                  Value = a.IdTask.ToString(),
                                  Text = a.Name
                              }).ToList();
            Options2 = _context.Users.Select(a =>
                              new SelectListItem
                              {
                                  Value = a.Id.ToString(),
                                  Text = a.firstName + " " + a.lastName
                              }).ToList();
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects.FirstOrDefaultAsync(m => m.IdProject == id);

            if (Project == null)
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

            _context.Attach(Project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(Project.IdProject))
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

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.IdProject == id);
        }
    }
}
