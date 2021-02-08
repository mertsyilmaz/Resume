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
    public class TechnologyController : Controller
    {
        private readonly ResumeContext _context;

        public TechnologyController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Technologies.ToListAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name")] Technology technology)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technology);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technology);
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
                return NotFound();
            var tecnology = await _context.Technologies.FirstOrDefaultAsync(t => t.Id == Id);
            if (tecnology == null)
                return NoContent();
            return View(tecnology);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
                return NotFound();
            var tecnology = await _context.Technologies.FindAsync(Id);
            if (tecnology == null)
                return NotFound();
            return View(tecnology);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Technology technology)
        {
            if (id != technology.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technology);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechnologyExist(technology.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(technology);
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
                return NotFound();
            var technology = await _context.Technologies.FirstOrDefaultAsync(t => t.Id == Id);
            if (technology == null)
                return NotFound();
            return View(technology);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Delete(int Id)
        {
            var technology = await _context.Technologies.FindAsync(Id);
            _context.Technologies.Remove(technology);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechnologyExist(int id)
        {
            return _context.Technologies.Any(t => t.Id == id);
        }
    }
}
