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
    public class SpecialtiesController : Controller {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public SpecialtiesController(ApplicationDbContext context, IWebHostEnvironment appEnvironment) {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> SpecialtiesList() {

            var specialties = await _context.Specialties
                .AsNoTracking()
                .ToListAsync();

            return View(specialties);
        }

        [HttpGet]
        public async Task<IActionResult> SpecialtyInfo(int? id) {

            var specialty = await _context.Specialties
                .Include(s => s.SpecialtySubjectAssignments)
                    .ThenInclude(ssa => ssa.Subject)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (specialty == null) {
                return NotFound();
            }

            return View(specialty);
        }

        [HttpGet]
        public IActionResult CreateSpecialty() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSpecialty([Bind("Id, Name")]Specialty specialty) {

            if (ModelState.IsValid) {
                _context.Add(specialty);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(SpecialtiesList));
            }

            return View(specialty);
        }

        [HttpGet]
        public async Task<IActionResult> SubjectsList() {

            var subjects = await _context.Subjects
                .AsNoTracking()
                .ToListAsync();

            return View(subjects);
        }

        [HttpGet]
        public IActionResult CreateSubject() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubject([Bind("Id, Name")] Subject subject) {

            if (ModelState.IsValid) {
                _context.Add(subject);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(SubjectsList));
            }

            return View(subject);
        }

        [HttpGet]
        public async Task<IActionResult> AddSubjects(int? id) {

            if (id == null) {
                return NotFound();
            }

            var specialty = await _context.Specialties.FirstOrDefaultAsync(s => s.Id == id);

            if (specialty == null) {
                return NotFound();
            }

            //ViewBag.Specialty = new { specialty.Id, specialty.Name };
            // TODO: разобраться почему не работают анонимные типы при обращении к их полям во View
            ViewBag.Specialty = (Id: specialty.Id, Name: specialty.Name);

            var subjects = await _context.Subjects
                .AsNoTracking()
                .ToListAsync();

            return View(subjects);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubjects(int specialtyId, int[] subjects) {

            if (subjects == null || subjects.Length == 0) {
                return RedirectToAction(nameof(SpecialtiesList));
            }

            foreach (var subject in subjects) {
                _context.SpecialtySubjectAssignments.Add(new SpecialtySubjectAssignment()
                    {SpecialtyId = specialtyId, SubjectId = subject});
            }

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(SpecialtiesList));
        }


    }
}
