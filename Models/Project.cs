using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Models
{
    public class Project
    {
        public Project()
        {
            Categories = new List<Category>();
            Technologies = new List<Technology>();
            Images = new List<ProjectImages>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.Date, ErrorMessage = "This field is not in the proper format.")]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Images")]
        public virtual List<ProjectImages> Images { get; set; }

        [Display(Name = "Categories")]
        public virtual IEnumerable<Category> Categories { get; set; }

        [Display(Name = "Technologies")]
        public virtual IEnumerable<Technology> Technologies { get; set; }
    }
}
