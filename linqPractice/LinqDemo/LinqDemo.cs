using System;
using System.Collections.Generic;
using System.Linq;

namespace linqPractice
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CourseId { get; set; }
        public double Marks { get; set; }
    }

    class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
    }

    public static class LinqDemo
    {
        public static void Run()
        {
            // ============= DATA SETUP =============
            List<Student> students = new List<Student>
            {
                new Student { Id = 1, Name = "Alice", Age = 20, CourseId = 1, Marks = 85 },
                new Student { Id = 2, Name = "Bob", Age = 17, CourseId = 2, Marks = 65 },
                new Student { Id = 3, Name = "Charlie", Age = 22, CourseId = 1, Marks = 95 },
                new Student { Id = 4, Name = "David", Age = 19, CourseId = 3, Marks = 45 },
                new Student { Id = 5, Name = "Eve", Age = 23, CourseId = 2, Marks = 75 },
                new Student { Id = 6, Name = "Frank", Age = 18, CourseId = 3, Marks = 55 }
            };

            List<Course> courses = new List<Course>
            {
                new Course { Id = 1, CourseName = "Mathematics" },
                new Course { Id = 2, CourseName = "Physics" },
                new Course { Id = 3, CourseName = "Computer Science" }
            };

            // 1️⃣ FILTERING & PROJECTION
            // =====================================
            Console.WriteLine("1️⃣ Filtering & Projection");

            // Filtering: select only students older than 18
            var adults = students.Where(s => s.Age > 18);
            Console.WriteLine("Adults: " + string.Join(", ", adults.Select(s => s.Name)));

            // Projection: select only names
            var names = students.Select(s => s.Name);
            Console.WriteLine("Names: " + string.Join(", ", names));

            // Anonymous projection: Name + Pass/Fail
            var results = students.Select(s => new
            {
                s.Name,
                Status = s.Marks >= 50 ? "Pass" : "Fail"
            });
            Console.WriteLine("Results: " + string.Join(", ", results.Select(r => $"{r.Name}={r.Status}")));

            // =====================================
            // 2️⃣ SORTING & ORDERING
            // =====================================
            Console.WriteLine("\n2️⃣ Sorting & Ordering");

            var ordered = students.OrderByDescending(s => s.Marks);
            Console.WriteLine("Top by marks: " + string.Join(", ", ordered.Select(s => $"{s.Name}({s.Marks})")));

            var multiOrder = students.OrderBy(s => s.Age).ThenByDescending(s => s.Marks);
            Console.WriteLine("Sorted by Age→Marks: " + string.Join(", ", multiOrder.Select(s => $"{s.Name}({s.Age}/{s.Marks})")));

            // =====================================
            // 3️⃣ GROUPING & AGGREGATION
            // =====================================
            Console.WriteLine("\n3️⃣ Grouping & Aggregation");

            var avgByCourse = from s in students
                              group s by s.CourseId into g
                              select new
                              {
                                  CourseId = g.Key,
                                  AverageMarks = g.Average(s => s.Marks)
                              };
            foreach (var c in avgByCourse)
                Console.WriteLine($"Course {c.CourseId}: Avg Marks = {c.AverageMarks:F2}");

            // =====================================
            // 4️⃣ JOINING
            // =====================================
            Console.WriteLine("\n4️⃣ Joining Students with Courses");

            var joined = from s in students
                         join c in courses on s.CourseId equals c.Id
                         select new { s.Name, c.CourseName };
            foreach (var j in joined)
                Console.WriteLine($"{j.Name} → {j.CourseName}");

            // =====================================
            // 5️⃣ SET OPERATIONS
            // =====================================
            Console.WriteLine("\n5️⃣ Set Operations");

            var list1 = new List<int> { 1, 2, 3, 4 };
            var list2 = new List<int> { 3, 4, 5, 6 };

            Console.WriteLine("Union: " + string.Join(", ", list1.Union(list2)));
            Console.WriteLine("Intersect: " + string.Join(", ", list1.Intersect(list2)));
            Console.WriteLine("Except: " + string.Join(", ", list1.Except(list2)));

            // =====================================
            // 6️⃣ QUANTIFIERS
            // =====================================
            Console.WriteLine("\n6️⃣ Quantifiers");

            Console.WriteLine("Any failed? " + students.Any(s => s.Marks < 50));
            Console.WriteLine("All adults? " + students.All(s => s.Age >= 18));
            Console.WriteLine("Contains ID 2? " + students.Select(s => s.Id).Contains(2));

            // =====================================
            // 7️⃣ DEFERRED vs IMMEDIATE EXECUTION
            // =====================================
            Console.WriteLine("\n7️⃣ Deferred Execution Example");

            var lazyQuery = students.Where(s => s.Marks > 70);
            students.Add(new Student { Id = 7, Name = "Grace", Age = 21, CourseId = 1, Marks = 90 });

            // The query includes Grace because it’s executed *now* (not when defined)
            Console.WriteLine("High scorers: " + string.Join(", ", lazyQuery.Select(s => s.Name)));

            // =====================================
            // 8️⃣ CONVERSIONS
            // =====================================
            Console.WriteLine("\n8️⃣ Conversions");

            var dict = students.ToDictionary(s => s.Id, s => s.Name);
            Console.WriteLine("Dictionary Keys: " + string.Join(", ", dict.Keys));

            // =====================================
            // 9️⃣ MISCELLANEOUS OPERATORS
            // =====================================
            Console.WriteLine("\n9️⃣ Miscellaneous Examples");

            // Pagination
            var page2 = students.Skip(2).Take(2);
            Console.WriteLine("Page 2: " + string.Join(", ", page2.Select(s => s.Name)));

            // SelectMany (flattening nested collections)
            var studentCourses = courses.SelectMany(c => students.Where(s => s.CourseId == c.Id)
                                                                 .Select(s => new { c.CourseName, s.Name }));
            Console.WriteLine("Flattened: " + string.Join(", ", studentCourses.Select(x => $"{x.Name}→{x.CourseName}")));

            Console.WriteLine("\n✅ END OF LINQ DEMO ✅");
        }
    }
}
