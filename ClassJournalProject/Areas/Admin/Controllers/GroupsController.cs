using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassJournalProject.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                .Include(g => g.Specialty)
                .Include(g => g.Curator)
                .AsNoTracking()
                .ToListAsync();

            return View(groups);
        }

        // GET: GroupsController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: GroupsController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: GroupsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
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
    }
}
