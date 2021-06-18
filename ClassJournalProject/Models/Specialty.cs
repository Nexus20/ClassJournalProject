using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassJournalProject.Models {
    public class Specialty {

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Group> Groups { get; set; }

        public ICollection<SpecialtySubjectAssignment> SpecialtySubjectAssignments { get; set; }
    }
}
