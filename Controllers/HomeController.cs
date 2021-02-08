using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResumeMvcCore.Models;
using ResumeMvcCore.Models.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ResumeContext _context;

        public HomeController(ResumeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var information = _context.Informations.SingleOrDefault();
            return View(information);
        }
    }
}
