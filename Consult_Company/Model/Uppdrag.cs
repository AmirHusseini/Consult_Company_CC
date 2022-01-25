using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Consulting_Company.Model
{
    public class Uppdrag
    {
        public Uppdrag()
        {
            Projects = new HashSet<Project>();
        }

        [Key]
        public int IdTask { get; set; }
        [Display(Name = "Task Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Priority")]
        [EnumDataType(typeof(Prio))]
        public Prio Priority { get; set; }
        [EnumDataType(typeof(TaskRole))]
        [Display(Name = "Task Role")]
        public TaskRole TaskRoles { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public enum Prio
        {
            Low = 1, Medium = 2, High = 3
        }
        public enum TaskRole
        {
            [Display(Name = "Backend Developer")]
            BackendDeveloper = 1,
            [Display(Name = "Frontend Developer")]
            FrontendDeveloper = 2, 
            Tester = 3, 
            Support = 4,
            [Display(Name = "Database Administrator")]
            DatabaseAdministrator = 5
        }
        public enum Uppdragstatus
        {
            [Display(Name = "Not Assigned")]
            NotAssigned = 1,
            [Display(Name = "In Process")]
            InProcess = 2,
            Finished = 3
        }
    }
}
