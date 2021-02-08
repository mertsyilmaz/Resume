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
    public class InformationController : Controller
    {
        private readonly ResumeContext _context;

        public InformationController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Informations.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var information = await _context.Informations.FirstOrDefaultAsync(i => i.Id == id);
            if (information == null)
                return NotFound();
            return View(information);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Surname,DateOfBirth,Title,Description,Address,Email,PhoneNumber,Location")] Information information)
        {
            if (ModelState.IsValid)
            {
                _context.Add(information);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(information);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var information = await _context.Informations.FirstOrDefaultAsync(i => i.Id == id);
            if (information == null)
                return NotFound();
            return View(information);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Surname,DateOfBirth,Title,Description,Address,Email,PhoneNumber,Location")] Information information)
        {
            if (id != information.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(information);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformationExists(id))
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
            return View(information);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var information = await _context.Informations.FindAsync(id);
            if (information == null)
                return NotFound();
            return View(information);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var information = await _context.Informations.FirstOrDefaultAsync(i => i.Id == id);
            _context.Remove(information);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformationExists(int id)
        {
            return _context.Informations.Any(i => i.Id == id);
        }
    }
}
