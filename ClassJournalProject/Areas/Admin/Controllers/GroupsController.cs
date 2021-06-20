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
    public class GroupsController : Controller {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _appEnvironment;

        public GroupsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment appEnvironment) {
            _context = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GroupsList() {

            var groups = await _context.Groups
                .Include(g => g.Students)
                .Include(g => g.Specialty)
                .Include(g => g.Curator)
                .AsNoTracking()
                .ToListAsync();

            return View(groups);
        }

        // GET: GroupsController/Details/5
        [HttpGet]
        public async Task<IActionResult> GroupInfo(int? id) {

            if (id == null) {
                return NotFound();
            }

            var group = await _context.Groups
                .Include(g => g.Students)
                    .ThenInclude(s => s.EducationLevel)
                .Include(g => g.Students)
                    .ThenInclude(s => s.StudentStatus)
                .Include(g => g.Specialty)
                .Include(g => g.Curator)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group == null) {
                return NotFound();
            }

            return View(group);
        }

        // GET: GroupsController/Create
        [HttpGet]
        public IActionResult CreateGroup() {

            ViewBag.SpecialtyId = new SelectList(_context.Specialties, "Id", "NameWithId");
            ViewBag.CuratorId = new SelectList(_context.Teachers, "Id", "FullName");

            return View();
        }

        // POST: GroupsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGroup([Bind("Id, Number, Year, SpecialtyId, CuratorId")] Group group) {

            if (ModelState.IsValid) {
                _context.Add(group);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(GroupsList));
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
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AssignStudents(int? id) {

            if (id == null) {
                return NotFound();
            }

            var group = await _context.Groups
                .Include(g => g.Specialty)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group == null) {
                return NotFound();
            }

            ViewBag.Group = group;

            var students = await _context.Students.ToListAsync();

            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult> AssignStudents(int groupId, string[] studentsIds) {

            if (studentsIds == null || studentsIds.Length == 0) {
                return RedirectToAction(nameof(GroupsList));
            }

            var studentsToAssign = from student in _context.Students
                                                    where studentsIds.Contains(student.Id)
                                                    select student;

            foreach (var studentToAssign in studentsToAssign) {
                studentToAssign.GroupId = groupId;
            }

            _context.Students.UpdateRange(studentsToAssign);
            await _context.SaveChangesAsync();

            //return RedirectToAction(nameof(GroupInfo), new {id = groupId});
            return RedirectToAction(nameof(GroupsList));
        }
    }
}
