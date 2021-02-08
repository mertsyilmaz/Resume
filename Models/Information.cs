using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Models
{
    public class Information
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [DataType(DataType.Date, ErrorMessage = "This field is not in the proper format.")]
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Birthday")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

    }
}
