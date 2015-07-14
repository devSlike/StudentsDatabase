using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDatabase.Entities
{
    public class TestWork
    {
        public Int32 TestWorkId { get; set; }
        public virtual Test Test { get; set; }
        public virtual User User { get; set; }
        public Int32 Score { get; set; }
        public TimeSpan Time { get; set; }
    }
}
