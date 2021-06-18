using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ClassJournalProject.Models {

    public class User : IdentityUser {

        public enum UserSex {
            Male,
            Female,
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Patronymic { get; set; }

        [Required]
        public string Phone { get; set; }

        public string FullName => $"{Surname} {Name} {Patronymic}";

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public DateTime DateOfEntry { get; set; }

        [Required]
        public UserSex Sex { get; set; }

    }
}
