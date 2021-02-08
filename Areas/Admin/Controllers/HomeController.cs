using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeMvcCore.Models.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin,Admin")]
    public class HomeController : Controller
    {
        private readonly ResumeContext context;

        public HomeController(ResumeContext context)
        {
            this.context = context;
        }
        
        public IActionResult Index()
        {
            int errorCount = context.NlogDBLog.Where(n => n.Level == "Error").Count();
            int warningCount = context.NlogDBLog.Where(n => n.Level == "Warn").Count();
            int messageCount = context.Messages.Count();

            ViewBag.MessageCount = messageCount;
            ViewBag.ErrorCount = errorCount;
            ViewBag.WarnCount = warningCount;
            return View();
        }
    }
}
