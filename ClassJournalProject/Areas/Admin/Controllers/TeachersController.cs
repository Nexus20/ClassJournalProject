using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassJournalProject.Areas.Admin.Models.ViewModels;
using ClassJournalProject.Data;
using ClassJournalProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClassJournalProject.Areas.Admin.Controllers {

    [Area("Admin")]
    public class TeachersController : Controller {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _appEnvironment;

        public TeachersController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment appEnvironment) {
            _context = context;
            _userManager = userManager;
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
        public async Task<IActionResult> TeacherInfo(string id) {

            var teacher = await _context.Teachers
                .Include(s => s.TeacherSubjectAssignments)
                .ThenInclude(tsa => tsa.Subject)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null) {
                return NotFound();
            }

            return View(teacher);
        }

        [HttpGet]
        public IActionResult CreateTeacher() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeacher(CreateTeacherViewModel model) {

            if (ModelState.IsValid) {

                var teacher = new Teacher() {
                    UserName = model.Login,
                    Name = model.Name,
                    Surname = model.Surname,
                    Patronymic = model.Patronymic,
                    PhoneNumber = model.Phone,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth,
                    DateOfEntry = DateTime.Now,
                    Sex = (User.UserSex)model.Sex,
                    Rank = Teacher.TeacherRank.SeniorLecturer
                };

                var result = await _userManager.CreateAsync(teacher, model.Password);

                if (result.Succeeded) {
                    await _userManager.AddToRoleAsync(teacher, "teacher");
                    return RedirectToAction(nameof(TeachersList));
                }
                foreach (var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                //_context.Add(teacher);
                //await _context.SaveChangesAsync();
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AddRemoveSubjects(string id) {

            if (id == null) {
                return NotFound();
            }

            var teacher = await _context.Teachers.FirstOrDefaultAsync(s => s.Id == id);

            if (teacher == null) {
                return NotFound();
            }

            ViewBag.Teacher = (Rank: teacher.Rank, Name: teacher.FullName);

            var existingSubjects = _context.TeacherSubjectAssignments
                .Include(ssa => ssa.Subject)
                .Where(tsa => tsa.TeacherId == id)
                .Select(tsa => tsa.Subject)
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
        public async Task<IActionResult> AddRemoveSubjects(string teacherId, int[] subjectsToAdd, int[] subjectsToRemove) {

            if ((subjectsToAdd == null || subjectsToAdd.Length == 0) && (subjectsToRemove == null || subjectsToRemove.Length == 0)) {
                return RedirectToAction(nameof(TeachersList));
            }

            if (subjectsToAdd != null) {
                foreach (var subject in subjectsToAdd) {
                    _context.TeacherSubjectAssignments.Add(new TeacherSubjectAssignment() { TeacherId = teacherId, SubjectId = subject });
                }
            }

            if (subjectsToRemove != null) {

                var teacherSubjectAssignmentsToRemove =
                    new List<TeacherSubjectAssignment>();

                foreach (var subjectToRemove in subjectsToRemove) {
                    teacherSubjectAssignmentsToRemove.Add(_context.TeacherSubjectAssignments.Find(teacherId, subjectToRemove));
                }

                _context.RemoveRange(teacherSubjectAssignmentsToRemove);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(TeacherInfo), new { id = teacherId });
        }


    }
}
