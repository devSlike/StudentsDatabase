using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDatabase.Entities
{
    public class Answer
    {
        public Int32 AnswerId { get; set; }
        public virtual Question Question { get; set; }
        public Boolean Correct { get; set; }
    }
}
