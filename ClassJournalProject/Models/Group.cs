using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassJournalProject.Models {
    public class Group {

        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public ushort Year { get; set; }

        [Required]
        [Display(Name = "Specialty")]
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        [Display(Name = "Curator")]
        public string CuratorId { get; set; }
        public Teacher Curator { get; set; }

        public ICollection<Student> Students;

        [Display(Name = "Students")]
        public int StudentsCount => Students?.Count ?? 0;

        public ICollection<Lesson> Lessons;
    }
}
