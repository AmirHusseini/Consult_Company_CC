using Consult_Company.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consult_Company.Pages.Admin
{
    public class adminModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public List<IdentityRole> roles { get; set; }
        public adminModel( RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            
        }
        
        public void OnGet()
        {
            roles = roleManager.Roles.ToList();
        }

        
        public async Task<IActionResult> OnPostCreate([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToPage();
                
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDelete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToPage();
                
            }
            else
                ModelState.AddModelError("", "No role found");
            return Page();
        }

    }
}
