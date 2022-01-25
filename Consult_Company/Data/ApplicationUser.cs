using Consulting_Company.Model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Consult_Company.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Projects = new HashSet<Project>();
        }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
