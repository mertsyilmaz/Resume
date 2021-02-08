using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeMvcCore.Models;
using ResumeMvcCore.Models.EntitiyFramework;
using ResumeMvcCore.Models.GoogleReCAPTCHA;
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
        private readonly ReCAPTCHAService _reCAPTCHAService;

        public ContactController(ResumeContext context,ReCAPTCHAService reCAPTCHAService)
        {
            _context = context;
            _reCAPTCHAService = reCAPTCHAService;
        }

        public IActionResult SendMessage()
        {
            var information = _context.Informations.FirstOrDefault();
            ViewBag.Contact = information;
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage([Bind("FullName,Email,Subject,Msg,Token")] Message message)
        {

            var recap = _reCAPTCHAService.Verify(message.Token);
            if (!recap.Result.success && recap.Result.score <= 0.5)
            {
                ModelState.AddModelError(string.Empty, "You are a bot");
                return View(message);
            }
            
            if (ModelState.IsValid)
            {
                message.Status = false;
                message.SendingDate = DateTime.Now;
                _context.Add(message);
                _context.SaveChanges();
                return RedirectToAction(nameof(SendMessage));
            }
            return View(message);
        }
    }
}
