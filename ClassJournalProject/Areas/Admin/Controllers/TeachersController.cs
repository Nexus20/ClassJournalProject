using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassJournalProject.Data;
using ClassJournalProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace ClassJournalProject.Areas.Admin.Controllers {

    [Area("Admin")]
    public class TeachersController : Controller {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public TeachersController(ApplicationDbContext context, IWebHostEnvironment appEnvironment) {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> TeachersList() {

            var teachers = await _context.Teachers
                .AsNoTracking()
                .ToListAsync();

            return View(teachers);
        }

        [HttpGet]
        public IActionResult CreateTeacher() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeacher([Bind("Id, Name")] Teacher teacher) {

            if (ModelState.IsValid) {
                _context.Add(teacher);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(TeachersList));
            }

            return View(teacher);
        }

    }
}
