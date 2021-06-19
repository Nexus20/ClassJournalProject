using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassJournalProject.Models {
    public class Group {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public string CuratorId { get; set; }
        public Teacher Curator { get; set; }

        public ICollection<Student> Students;

        public int StudentsCount => Students.Count;

        public ICollection<Lesson> Lessons;
    }
}
