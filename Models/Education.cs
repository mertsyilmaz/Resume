using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Models
{
    public class Education
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Faculty Name")]
        public string FacultyName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [DataType(DataType.Date, ErrorMessage = "This field is not in the proper format.")]
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Date of Start")]
        public DateTime DateOfStart { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Date of End")]
        [DataType(DataType.Date, ErrorMessage = "This field is not in the proper format.")]
        public DateTime DateOfEnd { get; set; }

        [Display(Name = "GPA")]
        public double GPA { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
