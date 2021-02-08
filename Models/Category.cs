using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Models
{
    public class Category
    {
        public Category()
        {
            Projects = new List<Project>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public virtual IEnumerable<Project> Projects { get; set; }
    }
}
