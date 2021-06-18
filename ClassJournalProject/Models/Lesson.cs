using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassJournalProject.Models {
    public class Lesson {

        public int Id { get; set; }
        public string Theme { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int TeacherSubjectAssignmentId { get; set; }
        public TeacherSubjectAssignment TeacherSubjectAssignment { get; set; }
    }
}
