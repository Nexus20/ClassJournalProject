using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassJournalProject.Models {
    public class Group {

        public int Id { get; set; }
        public string Name { get; set; }

        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public string CuratorId { get; set; }
        public Teacher Curator { get; set; }

        public ICollection<Student> Students;
    }
}
