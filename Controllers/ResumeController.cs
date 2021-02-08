using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeMvcCore.Models.EntitiyFramework;
using ResumeMvcCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Controllers
{
    [AllowAnonymous]
    public class ResumeController : Controller
    {
        private readonly ResumeContext _context;

        public ResumeController(ResumeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var information = _context.Informations.SingleOrDefault();
            var experiences = _context.Experiences.ToList();
            var educations = _context.Educations.ToList();
            var certificates = _context.Certificates.ToList();
            var skills = _context.Skills.ToList();

            ResumeCreateViewModel resumeCreateViewModel = new ResumeCreateViewModel
            {
                Skills = skills,
                Information = information,
                Experiences = experiences,
                Educations = educations,
                Certificates = certificates
            };
            return View(resumeCreateViewModel);
        }
    }
}
