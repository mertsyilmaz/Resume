using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Models
{
    public class Experience
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.Date, ErrorMessage = "This field is not in the proper format.")]
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Date of Start")]
        public DateTime DateOfStart { get; set; }

        [DataType(DataType.Date, ErrorMessage = "This field is not in the proper format.")]
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Date of End")]
        public DateTime DateOfEnd { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Current")]
        public bool IsCurrent { get; set; }

    }
}
