using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassJournalProject.Models {
    public class StudentEducationLevel {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
