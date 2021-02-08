using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeMvcCore.Models.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin")]
    public class LogController : Controller
    {
        private readonly ResumeContext context;

        public LogController(ResumeContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var logs = await context.NlogDBLog.ToListAsync();
            return View(logs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var log = await context.NlogDBLog.FirstOrDefaultAsync(l => l.Id == id);
            if (log == null)
            {
                return NotFound();
            }

            return View(log);
        }
    }
}
