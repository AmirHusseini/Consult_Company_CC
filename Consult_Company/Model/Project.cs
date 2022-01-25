using Consult_Company.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Consulting_Company.Model
{
    public class Project
    {
        

        [Key]
        public int IdProject{ get; set; }
        [Display(Name ="Project Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Task Name")]
        public int IdTask { get; set; }
        public Uppdrag Task { get; set; }
        [Display(Name = "Employee")]
        public string UserId { get; set; }
        public status Status { get; set; }
        public virtual ApplicationUser IdentUser { get; set; }
        public enum status
        {
            [Display(Name = "Not Assigned")]
            NotAssigned = 1,
            [Display(Name = "In Process")]
            InProcess = 2,            
            Finished = 3
        }
    }
}
