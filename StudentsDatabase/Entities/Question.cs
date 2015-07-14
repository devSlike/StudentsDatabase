using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDatabase.Entities
{
    public class Question
    {
        public Int32 QuestionId { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public String Text { get; set; }
    }
}
