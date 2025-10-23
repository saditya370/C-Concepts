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

            // ============= LINQ EXAMPLES =============
            Console.WriteLine("=== 1. All Students ===");
            var allStudents = from s in students select s;
            foreach (var s in allStudents)
                Console.WriteLine($"{s.Id} - {s.Name} ({s.Age})");

            Console.WriteLine("\n=== 2. Students Older than 18 ===");
            var adults = students.Where(s => s.Age > 18);
            foreach (var s in adults)
                Console.WriteLine(s.Name);

            Console.WriteLine("\n=== 3. Students Ordered by Marks Desc ===");
            var ordered = students.OrderByDescending(s => s.Marks);
            foreach (var s in ordered)
                Console.WriteLine($"{s.Name}: {s.Marks}");

            Console.WriteLine("\n=== 4. Select Names Only ===");
            var names = students.Select(s => s.Name);
            foreach (var name in names)
                Console.WriteLine(name);

            Console.WriteLine("\n=== 5. Students Grouped by CourseId ===");
            var grouped = students.GroupBy(s => s.CourseId);
            foreach (var group in grouped)
            {
                Console.WriteLine($"CourseId {group.Key}:");
                foreach (var s in group)
                    Console.WriteLine($"  {s.Name}");
            }

            Console.WriteLine("\n=== 6. Join Students with Courses ===");
            var joined = from s in students
                         join c in courses on s.CourseId equals c.Id
                         select new { s.Name, c.CourseName };
            foreach (var item in joined)
                Console.WriteLine($"{item.Name} → {item.CourseName}");

            Console.WriteLine("\n=== 7. Top 3 Students by Marks ===");
            var top3 = students.OrderByDescending(s => s.Marks).Take(3);
            foreach (var s in top3)
                Console.WriteLine($"{s.Name}: {s.Marks}");

            Console.WriteLine("\n=== 8. Average Marks ===");
            double avgMarks = students.Average(s => s.Marks);
            Console.WriteLine($"Average Marks: {avgMarks:F2}");

            Console.WriteLine("\n=== 9. Count of Students per Course ===");
            var countPerCourse = from s in students
                                 group s by s.CourseId into g
                                 select new { CourseId = g.Key, Count = g.Count() };
            foreach (var item in countPerCourse)
                Console.WriteLine($"Course {item.CourseId}: {item.Count} students");

            Console.WriteLine("\n=== 10. Check if Any Student Failed (<50 Marks) ===");
            bool anyFailed = students.Any(s => s.Marks < 50);
            Console.WriteLine($"Any failed? {anyFailed}");

            Console.WriteLine("\n=== 11. All Students Adults? ===");
            bool allAdults = students.All(s => s.Age >= 18);
            Console.WriteLine($"All adults? {allAdults}");

            Console.WriteLine("\n=== 12. Distinct Course IDs ===");
            var distinctCourses = students.Select(s => s.CourseId).Distinct();
            foreach (var id in distinctCourses)
                Console.WriteLine(id);

            Console.WriteLine("\n=== 13. Sum of All Marks ===");
            double totalMarks = students.Sum(s => s.Marks);
            Console.WriteLine($"Total Marks: {totalMarks}");

            Console.WriteLine("\n=== 14. Min and Max Marks ===");
            Console.WriteLine($"Lowest: {students.Min(s => s.Marks)}, Highest: {students.Max(s => s.Marks)}");

            Console.WriteLine("\n=== 15. Students Who Passed (>=50 Marks) ===");
            var passed = students.Where(s => s.Marks >= 50).Select(s => s.Name);
            foreach (var name in passed)
                Console.WriteLine(name);

            Console.WriteLine("\n=== 16. Anonymous Projection (Name + Status) ===");
            var status = students.Select(s => new
            {
                s.Name,
                Status = s.Marks >= 50 ? "Pass" : "Fail"
            });
            foreach (var s in status)
                Console.WriteLine($"{s.Name} → {s.Status}");

            Console.WriteLine("\n=== 17. Skip 2 and Take 2 (Pagination) ===");
            var paged = students.Skip(2).Take(2);
            foreach (var s in paged)
                Console.WriteLine(s.Name);

            Console.WriteLine("\n=== 18. Order by Age then Marks ===");
            var ordered2 = students.OrderBy(s => s.Age).ThenByDescending(s => s.Marks);
            foreach (var s in ordered2)
                Console.WriteLine($"{s.Name} ({s.Age}) - {s.Marks}");

            Console.WriteLine("\n=== 19. Students As Dictionary ===");
            var dict = students.ToDictionary(s => s.Id, s => s.Name);
            foreach (var kv in dict)
                Console.WriteLine($"{kv.Key}: {kv.Value}");

            Console.WriteLine("\n=== 20. Find First Student Below 60 Marks ===");
            var low = students.FirstOrDefault(s => s.Marks < 60);
            Console.WriteLine(low != null ? low.Name : "None found");

            Console.WriteLine("\n=== 21. Reverse the Order of IDs ===");
            var reversed = students.Select(s => s.Id).Reverse();
            foreach (var id in reversed)
                Console.WriteLine(id);

            Console.WriteLine("\n=== 22. Take While Marks > 60 ===");
            var whileMarks = students.TakeWhile(s => s.Marks > 60);
            foreach (var s in whileMarks)
                Console.WriteLine(s.Name);

            Console.WriteLine("\n=== 23. Union of Two Integer Lists ===");
            var list1 = new List<int> { 1, 2, 3, 4 };
            var list2 = new List<int> { 3, 4, 5, 6 };
            var union = list1.Union(list2);
            Console.WriteLine(string.Join(", ", union));

            Console.WriteLine("\n=== 24. Intersect and Except ===");
            var intersect = list1.Intersect(list2);
            var except = list1.Except(list2);
            Console.WriteLine("Common: " + string.Join(", ", intersect));
            Console.WriteLine("Unique to list1: " + string.Join(", ", except));

            Console.WriteLine("\n=== 25. Convert to JSON-like Anonymous Objects ===");
            var jsonLike = students.Select(s => new
            {
                s.Id,
                s.Name,
                s.Marks
            });
            foreach (var s in jsonLike)
                Console.WriteLine($"{{ Id: {s.Id}, Name: \"{s.Name}\", Marks: {s.Marks} }}");

            Console.WriteLine("\n=== END OF LINQ DEMO ===");
        }
    }
}
