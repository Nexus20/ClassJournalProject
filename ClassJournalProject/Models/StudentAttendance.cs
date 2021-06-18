using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassJournalProject.Models {
    public class StudentAttendance {

        public int Id { get; set; }

        public string StudentId { get; set; }
        public Student Student { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public enum StudentPresence {
            Present,
            Late,
            Absent,
            AbsentForGoodReason
        }

        public StudentPresence Presence { get; set; }

    }
}
