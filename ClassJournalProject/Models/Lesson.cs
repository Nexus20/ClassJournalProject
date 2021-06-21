using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassJournalProject.Models {
    public class Lesson {

        public int Id { get; set; }

        [Required]
        public string Theme { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        [Required]
        public int TeacherSubjectAssignmentId { get; set; }
        public TeacherSubjectAssignment TeacherSubjectAssignment { get; set; }
    }
}
