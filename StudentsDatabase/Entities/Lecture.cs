using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDatabase.Entities
{
    public class Lecture
    {
        public Int32 LectureId { get; set; }
        [Required, StringLength(100)]
        public String Name { get; set; }
        public virtual Category Category { get; set; }
        public String Description { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }

        public Lecture()
        {
            Teachers = new List<Teacher>();
        }
    }
}
