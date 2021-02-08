using ResumeMvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Models.ViewModels
{
    public class ResumeCreateViewModel
    {
        public Information Information { get; set; }

        public List<Education> Educations { get; set; }

        public List<Certificate> Certificates { get; set; }

        public List<Experience> Experiences { get; set; }

        public List<Skill> Skills { get; set; }
    }
}
