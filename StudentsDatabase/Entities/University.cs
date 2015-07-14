using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDatabase.Entities
{
    public class University
    {
        public Int32 UniversityId { get; set; }
        [Required, StringLength(100)]
        public String Name { get; set; }
    }
}
