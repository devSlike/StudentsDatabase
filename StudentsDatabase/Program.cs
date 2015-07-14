using StudentsDatabase.DataBaseInfrastructure;
using StudentsDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new StudentsContext();

            //Список людей, которые прошли тесты.
            Console.WriteLine("Список людей, которые прошли тесты");
            var report1 = context.Users.ToList();
            ShowUsersList(report1);

            //Список тех, кто прошли тесты успешно и уложились во время.
            Console.WriteLine("\n\nСписок тех, кто прошли тесты успешно и уложились во время");
            var report2 = from t in context.Users
                          where t.TestWorks.Where(x => x.Score >= x.Test.PassingScore && x.Time <= x.Test.MaxTime).Count() == t.TestWorks.Count
                          select t;
            ShowUsersList(report2);

            //Список людей, которые прошли тесты успешно и не уложились во время
            Console.WriteLine("\n\nСписок людей, которые прошли тесты успешно и не уложились во время");
            var report3 = from t in context.Users
                          where t.TestWorks.Where(x => x.Score >= x.Test.PassingScore && x.Time > x.Test.MaxTime).Count() > 0
                          select t;
            ShowUsersList(report3);

            //Список студентов по городам. (Например: из Львова: 10 студентов, из Киева: 20)
            Console.WriteLine("\n\nСписок студентов по городам");
            var report4 = from t in context.Users
                          group t by t.City into g
                          select new { City = g.Key, Count = g.Count() };
            foreach (var r in report4)
                Console.WriteLine(String.Format("{0}: {1} студентов", r.City.Name, r.Count.ToString()));

            //Список успешных студентов по городам.
            Console.WriteLine("\n\nСписок успешных студентов по городам");
            var report5 = from t in context.Users
                          where t.TestWorks.Count(x => x.Score >= x.Test.PassingScore && x.Time <= x.Test.MaxTime) == t.TestWorks.Count
                          group t by t.City into g
                          select new { City = g.Key, Users = g.Select(item => item) };
            try
            {
                foreach (var r in report5)
                {
                    Console.WriteLine(String.Format("Город {0}", r.City.Name));
                    ShowUsersList(r.Users);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(String.Format("Ошибка выполнения запроса", ex.Message));
            }

            //Результат для каждого студента - его баллы, время, баллы в процентах для каждой категории.
            Console.WriteLine("\n\nРезультат для каждого студента - его баллы, время, баллы в процентах для каждой категории");
            var report6 = from t in context.Users
                          select new
                          {
                              User = t,
                              TestsResults = (from tw in t.TestWorks
                                              select new
                                              {
                                                  Name = tw.Test.Name,
                                                  Score = tw.Score,
                                                  Time = tw.Time,
                                                  ScoreByCategory = from q in tw.Test.Answers
                                                                    group q by q.Question.Category into g
                                                                    select new { Category = g.Key, Percent = (Double)g.Count() / tw.Test.Answers.Count() * 100 }
                                              })
                          };
            try
            {
                foreach (var r in report6)
                {
                    Console.WriteLine(String.Format("Студент {0}", r.User.ToString()));
                    foreach (var tr in r.TestsResults)
                    {
                        Console.Write(String.Format("Тестовая работа {0}, баллы: {1}, время: {2}", tr.Name, tr.Score, tr.Time));
                        foreach (var cat in tr.ScoreByCategory)
                            Console.Write(String.Format("{0}: {1}%", cat.Category.Name, cat.Percent.ToString()));
                        Console.WriteLine();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(String.Format("Ошибка выполнения запроса", ex.Message));
            }

            //Рейтинг популярности вопросов в тестах (выводить количество использования данного вопроса в тестах)
            Console.WriteLine("\n\nРейтинг популярности вопросов в тестах");
            var report7 = from user in context.Users
                          from tw in user.TestWorks
                          from q in tw.Test.Answers
                          group q by q.Question into g
                          orderby g.Count() descending
                          select new { Question = g.Key, Count = g.Count() };
            foreach (var r in report7)
                Console.WriteLine(String.Format("Вопрос \"{0}\", кло-во использования {1}", r.Question.Text, r.Count.ToString()));

            //Рейтинг учителей по количеству лекций (Количество прочитанных лекций)
            Console.WriteLine("\n\nРейтинг учителей по количеству лекций");
            var report8 = (from t in context.Teachers
                           orderby t.Lectures.Count() descending
                           select t).ToList();
            for (var i = 0; i < report8.Count(); ++i)
            {
                var r = report8[i];
                Console.WriteLine(String.Format("№{0}. Учитель - {1}, кло-во лекций - {2}", i.ToString(), r.Name, r.Lectures.Count()));
            }

            //Средний бал тестов по категориям, отсортированый по убыванию.
            Console.WriteLine("\n\nСредний бал тестов по категориям, отсортированый по убыванию");
            var report9 = (from tw in context.TestWorks
                           group tw by tw.Test.Category into g
                           select new
                           {
                               Category = g.Key,
                               AverageScore = g.Average(x => x.Score)
                           }).OrderByDescending(x => x.AverageScore);
            foreach (var r in report9)
                Console.WriteLine(String.Format("Категория - {0}, средний балл - {1}", r.Category.Name, r.AverageScore.ToString()));

            //Рейтинг вопросов по набранным баллам
            Console.WriteLine("\n\nРейтинг вопросов по набранным баллам");
            var report10 = (from user in context.Users
                            from tw in user.TestWorks
                            from q in tw.Test.Answers
                            group q by q.Question into g
                            select new { Question = g.Key, Score = g.Count(x => x.Correct) }).OrderByDescending(x => x.Score).ToList();
            for (var i = 0; i < report10.Count(); ++i)
            {
                var r = report10[i];
                Console.WriteLine(String.Format("№{0} Вопрос - {1}, набрано баллов - {2}", i.ToString(), r.Question.Text, r.Score.ToString()));
            }
            Console.ReadKey();
        }

        private static void ShowUsersList(IEnumerable<User> users)
        {
            foreach (var u in users)
                Console.WriteLine(u.ToString());
        }
    }
}
