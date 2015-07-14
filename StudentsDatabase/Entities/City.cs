using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDatabase.Entities
{
    public class City
    {
        public Int32 CityId { get; set; }
        [Required, StringLength(100)]
        public String Name { get; set; }
    }
}
