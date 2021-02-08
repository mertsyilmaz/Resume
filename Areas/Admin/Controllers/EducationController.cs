using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeMvcCore.Models;
using ResumeMvcCore.Models.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin,Admin")]
    public class EducationController : Controller
    {
        private readonly ResumeContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EducationController(ResumeContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var educations = await _context.Educations.ToListAsync();
            return View(educations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Create([Bind("Id,SchoolName,FacultyName,DepartmentName,DateOfStart,DateOfEnd,Description,GPA,ImageFile")] Education education)
        {
            if (ModelState.IsValid)
            {
                education.ImageUrl = UploadImage(education);
                _context.Add(education);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(education);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var education = await _context.Educations.FirstOrDefaultAsync(e => e.Id == id);
            if (education == null)
                return NotFound();
            return View(education);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var education = await _context.Educations.FindAsync(id);
            if (education == null)
                return NotFound();
            return View(education);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SchoolName,FacultyName,DepartmentName,DateOfStart,DateOfEnd,Description,GPA,ImageUrl,ImageFile")] Education education)
        {
            if (id != education.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    if (education.ImageFile != null)
                    {
                        DeleteImage(education);
                        education.ImageUrl = UploadImage(education);
                    }
                    _context.Update(education);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationExists(id))
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
            return View(education);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var education = await _context.Educations.FirstOrDefaultAsync(e => e.Id == id);
            if (education == null)
                return NotFound();
            return View(education);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            DeleteImage(education);
            _context.Remove(education);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationExists(int id)
        {
            return _context.Educations.Any(e => e.Id == id);
        }

        private string UploadImage(Education education)
        {
            string uniqueFileName = null;
            if (education.ImageFile != null)
            {
                string uploadFolders = Path.Combine(_hostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + education.ImageFile.FileName;
                string filePath = Path.Combine(uploadFolders, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                education.ImageFile.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
        private void DeleteImage(Education education)
        {
            if (education.ImageUrl != null)
            {
                string filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", education.ImageUrl);
                System.IO.File.Delete(filePath);
            }
        }
    }
}
