using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeMvcCore.Models;
using ResumeMvcCore.Models.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin,Admin")]
    public class ExperienceController : Controller
    {
        private readonly ResumeContext _context;

        public ExperienceController(ResumeContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var experiences = await _context.Experiences.ToListAsync();
            return View(experiences);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Create([Bind("Id,CompanyName,DepartmentName,Title,DateOfStart,DateOfEnd,Description,IsCurrent")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(experience);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var experience = await _context.Experiences.FirstOrDefaultAsync(e => e.Id == id);
            if (experience == null)
                return NotFound();
            return View(experience);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var experinece = await _context.Experiences.FindAsync(id);
            if (experinece == null)
                return NotFound();
            return View(experinece);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,DepartmentName,Title,DateOfStart,DateOfEnd,Description,Status")] Experience experience)
        {
            if (id != experience.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienceExists(id))
                    { return NotFound(); }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(experience);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var experience = await _context.Experiences.FirstOrDefaultAsync(e => e.Id == id);
            if (experience == null)
                return NotFound();
            return View(experience);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ExperienceExists(int id)
        {
            return _context.Experiences.Any(e => e.Id == id);
        }
    }
}

