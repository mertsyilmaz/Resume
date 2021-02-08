﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Models.ViewModels
{
    public class ProjectUpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public IFormFileCollection Images { get; set; }

        public int[] TechnologyId { get; set; }

        public int[] CategoryId { get; set; }
    }
}
