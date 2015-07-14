using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDatabase.Entities
{
    public class Category
    {
        public Int32 CategoryId { get; set; }
        [Required, StringLength(100)]
        public String Name { get; set; }
    }
}
