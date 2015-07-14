using StudentsDatabase.DataBaseInfrastructure;
using StudentsDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDatabase.DatabaseInfrastructure
{
    public static class DataGenerator
    {
        private static Random random = new Random();
        public static StudentsContext context { get; set; }
        
        public static List<Category> GetCategoties()
        {
            var categories = new List<Category>
            {
                new Category { Name = ".Net" },
                new Category { Name = "JS" },
                new Category { Name = "PHP" },
                new Category { Name = "DB" },
                new Category { Name = "OOP" },
                new Category { Name = "English" }
            };
            return categories;
        }

        public static List<City> GetCities()
        {
            var cities = new List<City>
            {
                new City { Name = "Киев" },
                new City { Name = "Львов" },
                new City { Name = "Днепропкртовск" },
                new City { Name = "Одесса" },
                new City { Name = "Кривой Рог" },
                new City { Name = "Харьков" }
            };
            return cities;
        }

        public static List<University> GetUniversities()
        {
            var universities = new List<University>
            {
                new University { Name = "КНУ" },
                new University { Name = "КПИ" },
                new University { Name = "ХПИ" },
                new University { Name = "ОНУ" },
                new University { Name = "КТУ" },
                new University { Name = "ХТУ" }
            };
            return universities;
        }

        public static List<Teacher> GetTeachers(Int32 count)
        {
            var teachers = new List<Teacher>();
            for (var i = 0; i < count; ++i)
            {
                var teacher = new Teacher
                {
                    Name = String.Format("Учитель{0}", i.ToString())
                };
                teachers.Add(teacher);
            }
            return teachers;
        }

        public static List<Lecture> GetLectures(Int32 count, IList<Category> categories)
        {
            var lectures = new List<Lecture>();
            for (var i = 0; i < count; ++i)
            {
                var category = categories[random.Next(categories.Count)];
                var teacher = new Lecture
                {
                    Category = category,
                    Name = String.Format("Учитель{0}", i.ToString()),
                    Description = String.Format("Лекция из категории {0}", category.Name)
                };
                lectures.Add(teacher);
            }
            return lectures;
        }

        public static void SetLecturesToTeachers()
        {
            var lectures = context.Lectures.ToList();
            var teachers = context.Teachers.ToList();
            foreach (var teacher in teachers)
            {
                var countLections = random.Next(lectures.Count);
                for (var i = 0; i < countLections; ++i)
                {
                    var lectuteIndex = random.Next(lectures.Count);
                    var lecture = lectures[lectuteIndex];
                    teacher.Lectures.Add(lecture);
                    lecture.Teachers.Add(teacher);
                }
            }
        }

        public static List<Question> GetQuestions(Int32 count, IList<Category> categories)
        {
            var questions = new List<Question>();
            for (var i = 0; i < count; ++i)
            {
                var category = categories[random.Next(categories.Count)];
                var question = new Question
                {
                    Category = category,
                    Text = String.Format("Текст вопроса {0} из категории {1}", i.ToString(), category.Name)
                };
                questions.Add(question);
            }
            return questions;
        }

        private static TestWork GetTestWorkForUserAndCategory(User user, IList<Question> questions)
        {
            var answers = GetAnswers(40, questions);
            var test = new Test
                {
                    Category = user.Category,
                    MaxTime = new TimeSpan(1, 0, 0),
                    Name = String.Format("Тест из категории {0}", user.Category.Name),
                    PassingScore = 20,
                    Answers = answers

                };
            context.Tests.Add(test);
            var testWork = new TestWork
            {
                Test = test,
                Time = new TimeSpan(random.Next(2), random.Next(60), random.Next(60)),
                User = user
            };
            testWork.Score = testWork.Test.Answers.Count(x => x.Correct);
            context.TestWorks.Add(testWork);
            context.SaveChanges();
            var tmp = context.TestWorks.ToList();
            return tmp.Last();
        }

        private static List<Answer> GetAnswers(Int32 count, IList<Question> questuins)
        {
            var answers = new List<Answer>();
            for (var i = 0; i < count; ++i)
            {
                var index = random.Next(questuins.Count);
                var answer = new Answer
                {
                    Question = questuins[index],
                    Correct = Convert.ToBoolean(random.Next(0, 2))
                };
                answers.Add(answer);
            }
            return answers;
        }

        public static List<User> GetUsers(Int32 count)
        {
            var cities = context.Cities.ToList();
            var universities = context.Universities.ToList();
            var categories = context.Categories.ToList();
            var questions = context.Questions.ToList();

            var users = new List<User>();
            var testCategories = GetTestsCategories(categories);
            for (var i = 0; i < count; ++i)
            {
                var user = new User
                {
                    Age = random.Next(18, 35),
                    City = cities[random.Next(cities.Count)],
                    University = universities[random.Next(universities.Count)],
                    Email = String.Format("email_user{0}@mail.com", i.ToString()),
                    Name = String.Format("Имя{0}", i.ToString()),
                    Category = testCategories[random.Next(testCategories.Count)],
                };
                users.Add(user);
            }
            return users;
        }

        public static List<TestWork> GetTestWorks(IList<User> users)
        {
            var testWorks = new List<TestWork>();
            foreach (var user in users)
            {
                var questionsForCategory = GetQuestionsForTestCategory(user.Category);
                for (var j = 0; j < 4; ++j)
                {
                    var testWork = GetTestWorkForUserAndCategory(user, questionsForCategory);
                    context.TestWorks.Add(testWork);
                    context.SaveChanges();
                }
            }
            return testWorks;
        }

        private static List<Category> GetQuestionCategories(Category testCategory, IList<Category> allCategories)
        {
            var categories = new List<Category>
            {
                testCategory,
                allCategories.FirstOrDefault(x => x.Name == "DB"),
                allCategories.FirstOrDefault(x => x.Name == "OOP"),
                allCategories.FirstOrDefault(x => x.Name == "English")
            };
            return categories;
        }

        private static List<Question> GetQuestionsForTestCategory(Category testCategory)
        {
            var allCategories = context.Categories.ToList();
            var allQuestions = context.Questions.ToList();
            var categories = GetQuestionCategories(testCategory, allCategories);
            var questions = (from q in allQuestions
                            where categories.Exists(x => x.Name == q.Category.Name)
                            select q).ToList();
            return questions;
        }

        private static List<Category> GetTestsCategories(List<Category> allCategories)
        {
            var categories = new List<Category>
            {
                allCategories.FirstOrDefault(x => x.Name == ".Net"),
                allCategories.FirstOrDefault(x => x.Name == "JS"),
                allCategories.FirstOrDefault(x => x.Name == "PHP")
            };
            return categories;
        }
    }
}
