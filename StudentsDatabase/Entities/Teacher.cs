using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDatabase.Entities
{
    public class Teacher
    {
        public Int32 TeacherId { get; set; }
        [Required, StringLength(100)]
        public String Name { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }

        public Teacher()
        {
            Lectures = new List<Lecture>();
        }
    }
}
