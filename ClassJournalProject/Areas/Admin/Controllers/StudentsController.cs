using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassJournalProject.Data;
using ClassJournalProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClassJournalProject.Areas.Admin.Controllers {

    [Area("Admin")]
    public class StudentsController : Controller {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _appEnvironment;

        public StudentsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment appEnvironment) {
            _context = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> StudentsList() {

            var students = await _context.Students
                .Include(g => g.Group)
                .Include(g => g.EducationLevel)
                .Include(g => g.StudentStatus)
                .AsNoTracking()
                .ToListAsync();

            return View(students);
        }

        // GET: GroupsController/Details/5
        [HttpGet]
        public IActionResult Details(int id) {
            return View();
        }

        // GET: GroupsController/Create
        [HttpGet]
        public IActionResult CreateStudent() {

            ViewBag.SpecialtyId = new SelectList(_context.Specialties, "Id", "NameWithId");
            ViewBag.CuratorId = new SelectList(_context.Teachers, "Id", "FullName");

            return View();
        }

        // POST: GroupsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent([Bind("Id, Number, Year, SpecialtyId, CuratorId")] Group group) {

            if (ModelState.IsValid) {
                _context.Add(group);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(StudentsList));
            }

            return View(group);
        }

        // GET: GroupsController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: GroupsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: GroupsController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: GroupsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                return RedirectToAction();
            }
            catch {
                return View();
            }
        }



        [HttpGet]
        public async Task<IActionResult> StudentsStatusesList() {

            var studentStatuses = await _context.StudentStatuses
                .AsNoTracking()
                .ToListAsync();

            return View(studentStatuses);
        }

        [HttpGet]
        public IActionResult CreateStudentStatus() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudentStatus([Bind("Id, Name")] StudentStatus studentStatus) {

            if (ModelState.IsValid) {
                _context.Add(studentStatus);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(StudentsStatusesList));
            }

            return View(studentStatus);
        }


        [HttpGet]
        public async Task<IActionResult> StudentsEducationLevelsList() {

            var studentEducationLevels = await _context.StudentEducationLevels
                .AsNoTracking()
                .ToListAsync();

            return View(studentEducationLevels);
        }

        [HttpGet]
        public IActionResult CreateStudentEducationLevel() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudentEducationLevel([Bind("Id, Name")] StudentEducationLevel studentEducationLevel) {

            if (ModelState.IsValid) {
                _context.Add(studentEducationLevel);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(StudentsEducationLevelsList));
            }

            return View(studentEducationLevel);
        }
    }
}
