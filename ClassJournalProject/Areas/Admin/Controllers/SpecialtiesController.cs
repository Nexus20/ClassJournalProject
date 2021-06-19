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
        public async Task<IActionResult> AddRemoveSubjects(int? id) {

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

            var existingSubjects = _context.SpecialtySubjectAssignments
                .Include(ssa => ssa.Subject)
                .Where(ssa => ssa.SpecialtyId == id)
                .Select(ssa => ssa.Subject)
                .AsNoTracking()
                .ToList();


            var nonExistingSubjects = _context.Subjects
                .AsNoTracking()
                .ToList()
                .Except(existingSubjects)
                .ToList();

            var model = new Dictionary<string, List<Subject>> {
                {"existingSubjects", existingSubjects}, {"nonExistingSubjects", nonExistingSubjects}
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRemoveSubjects(int specialtyId, int[] subjectsToAdd, int[] subjectsToRemove) {

            if ((subjectsToAdd == null || subjectsToAdd.Length == 0) && (subjectsToRemove == null || subjectsToRemove.Length == 0)) {
                return RedirectToAction(nameof(SpecialtiesList));
            }

            if (subjectsToAdd != null) {
                foreach (var subject in subjectsToAdd) {
                    _context.SpecialtySubjectAssignments.Add(new SpecialtySubjectAssignment()
                        {SpecialtyId = specialtyId, SubjectId = subject});
                }
            }

            if (subjectsToRemove != null) {

                var specialtySubjectAssignmentsToRemove =
                    new List<SpecialtySubjectAssignment>();

                foreach (var subjectToRemove in subjectsToRemove) {
                    specialtySubjectAssignmentsToRemove.Add(_context.SpecialtySubjectAssignments.Find(specialtyId, subjectToRemove));
                }

                _context.RemoveRange(specialtySubjectAssignmentsToRemove);

                //_context.SpecialtySubjectAssignments.Where(ssa => ssa.SpecialtyId == specialtyId)
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(SpecialtyInfo), new {id = specialtyId});
        }


    }
}
