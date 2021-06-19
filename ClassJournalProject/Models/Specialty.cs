using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClassJournalProject.Models {
    public class Specialty {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string NameWithId => $"{Id} {Name}";

        public ICollection<Group> Groups { get; set; }

        public ICollection<SpecialtySubjectAssignment> SpecialtySubjectAssignments { get; set; }
    }
}
