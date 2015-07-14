using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using StudentsDatabase.Entities;

namespace StudentsDatabase.DataBaseInfrastructure
{
    public class StudentsContext : DbContext
    {
        public StudentsContext()
            : base("StudentsDatabase")
        {
            Database.SetInitializer<StudentsContext>(new StudentsDBInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<TestWork> TestWorks { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
