using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Message")]
        public string Msg { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime SendingDate { get; set; }
    }
}
