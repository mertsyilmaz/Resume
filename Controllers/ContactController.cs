using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeMvcCore.Models;
using ResumeMvcCore.Models.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeMvcCore.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        private readonly ResumeContext _context;

        public ContactController(ResumeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var information = _context.Informations.FirstOrDefault();
            ViewBag.Contact = information;
            return View();
        }

        public IActionResult SendMessage([Bind("FullName,Email,Subject,Msg")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.Status = false;
                message.SendingDate = DateTime.Now;
                _context.Add(message);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }
    }
}
