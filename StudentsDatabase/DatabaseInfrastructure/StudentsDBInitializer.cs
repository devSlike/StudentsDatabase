using StudentsDatabase.DatabaseInfrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDatabase.DataBaseInfrastructure
{
    //public class StudentsDBInitializer : CreateDatabaseIfNotExists<StudentsContext>
    public class StudentsDBInitializer : DropCreateDatabaseAlways<StudentsContext>
    {
        protected override void Seed(StudentsContext context)
        {
            DataGenerator.context = context;
            DataGenerator.GetCategoties().ForEach(item => context.Categories.Add(item));
            context.SaveChanges();
            DataGenerator.GetCities().ForEach(item => context.Cities.Add(item));
            context.SaveChanges();
            DataGenerator.GetUniversities().ForEach(item => context.Universities.Add(item));
            context.SaveChanges();
            DataGenerator.GetTeachers(10).ForEach(item => context.Teachers.Add(item));
            context.SaveChanges();
            DataGenerator.GetLectures(30, context.Categories.ToList()).ForEach(item => context.Lectures.Add(item));
            context.SaveChanges();
            DataGenerator.SetLecturesToTeachers();
            context.SaveChanges();
            DataGenerator.GetQuestions(150, context.Categories.ToList()).ForEach(item => context.Questions.Add(item));
            context.SaveChanges();
            DataGenerator.GetUsers(50).ForEach(item => context.Users.Add(item));
            context.SaveChanges();
            DataGenerator.GetTestWorks(context.Users.ToList()).ForEach(item => context.TestWorks.Add(item));
            context.SaveChanges();
        }
    }
}