using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassJournalProject.Models {

    public class Teacher : User {

        public Group Group { get; set; }

        public ICollection<TeacherSubjectAssignment> TeacherSubjectAssignments { get; set; }

        public enum TeacherRank {
            SeniorLecturer,
            Docent,
            Professor
        }

        public TeacherRank Rank { get; set; }

    }

}
