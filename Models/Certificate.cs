using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Models
{
    public class Certificate
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Certificate Name")]
        public string Name { get; set; }

        [DataType(DataType.Date, ErrorMessage = "This field is not in the proper format.")]
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Date of issue")]
        public DateTime DateOfIssue { get; set; }

        [Display(Name = "School/Company Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string SchoolName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
