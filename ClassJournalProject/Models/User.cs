using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ClassJournalProject.Models {

    public class User : IdentityUser {

        public enum UserSex {
            Male,
            Female,
        }

        [Required]
        [Display(Name="Login")]
        public override string UserName { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public override string PhoneNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Patronymic { get; set; }

        [Display(Name = "Full name")]
        public string FullName => $"{Surname} {Name} {Patronymic}";

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Date of entry")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfEntry { get; set; }

        [Required]
        public UserSex Sex { get; set; }
    }
}
