using System.ComponentModel.DataAnnotations;

namespace ClassJournalProject.Models {
    public class Student : User {

        public enum StudentEducationForm {
            Budget,
            Contract
        }

        [Required]
        public StudentEducationForm EducationForm { get; set; }

        public int StudentEducationLevelId { get; set; }
        public StudentEducationLevel EducationLevel { get; set; }

        public int StudentStatusId { get; set; }
        public StudentStatus StudentStatus { get; set; }
    }
}
