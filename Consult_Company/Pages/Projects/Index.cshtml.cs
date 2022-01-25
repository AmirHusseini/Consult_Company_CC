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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get;set; }
        public List<Uppdrag> Uppdrags { get; set; }
        public List<ApplicationUser> ApplicationUsers { get; set; }
        public async Task OnGetAsync()
        {
            
            Project = await _context.Projects.ToListAsync();
            Uppdrags = _context.Uppdrags.ToList();
            ApplicationUsers = _context.Users.ToList();

            foreach (var item in Project)
            {

                foreach (var item2 in Uppdrags)
                {

                    if (item.IdTask == item2.IdTask)
                    {
                        item.Task = new Uppdrag();
                        item.Task = item2 as Uppdrag;
                    }

                }
                foreach (var item3 in ApplicationUsers)
                {
                    if (item.UserId == item3.Id)
                    {
                        item.IdentUser = new ApplicationUser();
                        item.IdentUser = item3 as ApplicationUser;
                    }
                }
            }
        
        }
    }
}
