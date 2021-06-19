using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassJournalProject.Models {
    public class SpecialtySubjectAssignment {

        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

    }
}
