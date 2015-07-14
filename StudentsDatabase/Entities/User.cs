using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentsDatabase.Entities
{
    public class User
    {
        public Int32 UserId { get; set; }
        [Required, StringLength(100)]
        public String Name { get; set; }
        [Required, StringLength(100)]
        public String Email { get; set; }
        [Required, Range(18, 35)]
        public Int32 Age { get; set; }
        public virtual City City { get; set; }
        public virtual University University { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<TestWork> TestWorks { get; set; }

        public User()
        {
            TestWorks = new List<TestWork>();
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}", Name, Age.ToString(), City.Name);
        }
    }
}
