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
    public class CertificateController : Controller
    {
        private readonly ResumeContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CertificateController(ResumeContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Certificates.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,DateOfIssue,SchoolName,Description,ImageFile")] Certificate certificate)
        {
            if (ModelState.IsValid)
            {
                certificate.ImageUrl = UploadImage(certificate);
                _context.Add(certificate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(certificate);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var certificate = await _context.Certificates.FirstOrDefaultAsync(c => c.Id == id);
            if (certificate == null)
                return NotFound();
            return View(certificate);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var certificate = await _context.Certificates.FindAsync(id);
            if (certificate == null)
                return NotFound();
            return View(certificate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateOfIssue,SchoolName,Description,ImageUrl,ImageFile")] Certificate certificate)
        {
            if (id != certificate.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    if (certificate.ImageFile != null)
                    {
                        DeleteImage(certificate);
                        certificate.ImageUrl = UploadImage(certificate);
                    }
                    _context.Update(certificate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificateExists(id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(certificate);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var certificate = await _context.Certificates.FirstOrDefaultAsync(c => c.Id == id);
            if (certificate == null)
                return NotFound();
            return View(certificate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var certificate = await _context.Certificates.FindAsync(id);
            DeleteImage(certificate);
            _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CertificateExists(int id)
        {
            return _context.Certificates.Any(c => c.Id == id);
        }

        private string UploadImage(Certificate certificate)
        {
            string uniqueFileName = null;
            if (certificate.ImageFile != null)
            {
                string uploadFolders = Path.Combine(_hostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + certificate.ImageFile.FileName;
                string filePath = Path.Combine(uploadFolders, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                certificate.ImageFile.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
        private void DeleteImage(Certificate certificate)
        {
            if (certificate.ImageUrl != null)
            {
                string filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", certificate.ImageUrl);
                System.IO.File.Delete(filePath);
            }
        }
    }
}
