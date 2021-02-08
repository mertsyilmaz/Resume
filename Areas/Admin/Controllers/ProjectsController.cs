using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResumeMvcCore.Models;
using ResumeMvcCore.Models.EntitiyFramework;
using ResumeMvcCore.Models.ViewModels;

namespace ResumeMvcCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin,Admin")]
    public class ProjectsController : Controller
    {
        private readonly ResumeContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProjectsController(ResumeContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Admin/Projects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.ToListAsync());
        }

        // GET: Admin/Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.Include(p => p.Categories).Include(p => p.Technologies).Include(p => p.Images).FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            List<SelectListItem> categories = new List<SelectListItem>();
            List<SelectListItem> technologies = new List<SelectListItem>();
            List<ProjectImages> images = new List<ProjectImages>();
            foreach (var category in _context.Categories.ToList())
            {
                if (project.Categories.Contains(category))
                {
                    categories.Add(new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name,
                        Selected = true
                    });
                }
                else
                {
                    categories.Add(new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name,
                        Selected = false
                    });
                }
            }

            foreach (var technology in _context.Technologies.ToList())
            {
                if (project.Technologies.Contains(technology))
                {
                    technologies.Add(new SelectListItem
                    {
                        Value = technology.Id.ToString(),
                        Text = technology.Name,
                        Selected = true
                    });
                }
                else
                {
                    technologies.Add(new SelectListItem
                    {
                        Value = technology.Id.ToString(),
                        Text = technology.Name,
                        Selected = false
                    });
                }
            }

            foreach (var image in project.Images)
            {
                images.Add(image);
            }

            ViewBag.Technologies = technologies;
            ViewBag.Categories = categories;
            ViewBag.Images = images;

            return View(project);
        }

        // GET: Admin/Projects/Create
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();
            var technologies = _context.Technologies.ToList();

            ViewBag.Technologies = new SelectList(technologies, "Id", "Name");
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Create(ProjectCreateViewModel projectCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                Project project = new Project
                {
                    Name = projectCreateViewModel.Name,
                    Description = projectCreateViewModel.Description,
                    Date = projectCreateViewModel.Date,

                    Categories = _context.Categories.Where(c => projectCreateViewModel.CategoryId.Contains(c.Id)).ToList(),

                    Technologies = _context.Technologies.Where(c => projectCreateViewModel.TechnologyId.Contains(c.Id)).ToList()
                };

                if (projectCreateViewModel.Images != null)
                {
                    foreach (var file in projectCreateViewModel.Images)
                    {
                        var image = new ProjectImages()
                        {
                            ImageName = UploadImages(file)
                        };
                        project.Images.Add(image);
                    }
                }
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var categories = _context.Categories.ToList();
            var technologies = _context.Technologies.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", projectCreateViewModel.CategoryId);
            ViewBag.Technologies = new SelectList(technologies, "Id", "Name", projectCreateViewModel.TechnologyId);
            return View(projectCreateViewModel);
        }

        // GET: Admin/Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project project = await _context.Projects.Include(p => p.Categories).Include(p => p.Technologies).Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            List<SelectListItem> categories = new List<SelectListItem>();
            List<SelectListItem> technologies = new List<SelectListItem>();
            List<ProjectImages> images = new List<ProjectImages>();
            foreach (var category in _context.Categories.ToList())
            {
                if (project.Categories.Contains(category))
                {
                    categories.Add(new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name,
                        Selected = true
                    });
                }
                else
                {
                    categories.Add(new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name,
                        Selected = false
                    });
                }
            }

            foreach (var technology in _context.Technologies.ToList())
            {
                if (project.Technologies.Contains(technology))
                {
                    technologies.Add(new SelectListItem
                    {
                        Value = technology.Id.ToString(),
                        Text = technology.Name,
                        Selected = true
                    });
                }
                else
                {
                    technologies.Add(new SelectListItem
                    {
                        Value = technology.Id.ToString(),
                        Text = technology.Name,
                        Selected = false
                    });
                }
            }

            foreach (var image in project.Images)
            {
                images.Add(image);
            }

            ViewBag.Technologies = technologies;
            ViewBag.Categories = categories;
            ViewBag.Images = images;

            ProjectCreateViewModel projectCreateViewModel = new ProjectCreateViewModel()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Date = project.Date
            };
            return View(projectCreateViewModel);
        }

        // POST: Admin/Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Edit(ProjectCreateViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                var project = await _context.Projects.Include(p => p.Categories).Include(p => p.Technologies).Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == projectViewModel.Id);
                try
                {
                    if (projectViewModel.Images != null && projectViewModel.Images.Count > 0)
                    {
                        if (project.Images != null)
                        {
                            foreach (var image in project.Images)
                            {
                                DeleteImage(image.ImageName);
                            }
                        }
                        List<ProjectImages> images = new List<ProjectImages>();
                        foreach (var file in projectViewModel.Images)
                        {
                            var image = new ProjectImages()
                            {
                                ImageName = UploadImages(file)
                            };
                            images.Add(image);
                        }
                        project.Images = images;
                    }
                    project.Categories = _context.Categories.Where(c => projectViewModel.CategoryId.Contains(c.Id)).ToList();

                    project.Technologies = _context.Technologies.Where(c => projectViewModel.TechnologyId.Contains(c.Id)).ToList();

                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(projectViewModel.Id))
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
            var categories = _context.Categories.ToList();
            var technologies = _context.Technologies.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", projectViewModel.CategoryId);
            ViewBag.Technologies = new SelectList(technologies, "Id", "Name", projectViewModel.TechnologyId);
            return View(projectViewModel);
        }

        // GET: Admin/Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Admin/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.Include(p => p.Categories).Include(p => p.Technologies).Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
            if (project.Images != null && project.Images.Count > 0)
            {
                foreach (var file in project.Images)
                {
                    DeleteImage(file.ImageName);
                }
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
        private string UploadImages(IFormFile file)
        {
            string uniqueFileName = null;
            if (file != null)
            {
                string uploadFolders = Path.Combine(_hostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadFolders, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
        private void DeleteImage(string fileName)
        {
            if (fileName != null)
            {
                string filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);
                System.IO.File.Delete(filePath);
            }
        }
    }
}
