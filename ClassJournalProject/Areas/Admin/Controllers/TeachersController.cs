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

    }
}
