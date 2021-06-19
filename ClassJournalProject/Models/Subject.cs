using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassJournalProject.Models {
    public class Subject {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<SpecialtySubjectAssignment> SpecialtySubjectAssignments { get; set; }
        public ICollection<TeacherSubjectAssignment> TeacherSubjectAssignments { get; set; }
    }
}
