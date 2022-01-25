using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Consult_Company.Data;
using Consulting_Company.Model;

namespace Consult_Company.Pages.Projekter
{
    public class CreateModel : PageModel
    {
        private readonly Consult_Company.Data.ApplicationDbContext _context;
        public List<SelectListItem> Options { get; set; }
        public List<SelectListItem> Options2 { get; set; }
        public CreateModel(Consult_Company.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
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
            return Page();
        }

        [BindProperty]
        public Project Project { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Projects.Add(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
