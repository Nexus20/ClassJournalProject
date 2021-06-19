using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassJournalProject.Models {
    public class Subject : IEquatable<Subject> {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<SpecialtySubjectAssignment> SpecialtySubjectAssignments { get; set; }
        public ICollection<TeacherSubjectAssignment> TeacherSubjectAssignments { get; set; }

        public bool Equals(Subject other) {
            if (other == null) return false;
            return Id == other.Id && Name == other.Name;
        }

        public override bool Equals(object obj) => Equals(obj as Subject);
        public override int GetHashCode() => (Id, Name).GetHashCode();
    }
}
