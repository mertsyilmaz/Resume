using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Models
{
    public class ProjectImages
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ImageName { get; set; }
        public Project Project { get; set; }
    }
}
