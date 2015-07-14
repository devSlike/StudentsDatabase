using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDatabase.Entities
{
    public class Test
    {
        public Int32 TestId { get; set; }
        [Required, StringLength(100)]
        public String Name { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public TimeSpan MaxTime { get; set; }
        public Int32 PassingScore { get; set; }
    }
}
