using System;
using System.Collections.Generic;
using System.Linq;

namespace linqPractice
{
    public static class IEnumerableVsIQueryableDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== ⚙️ IEnumerable vs IQueryable DEMO =====\n");

            // Sample data: In-memory student list
            List<StudentLite> students = new List<StudentLite>
            {
                new StudentLite { Id = 1, Name = "Alice", Age = 20 },
                new StudentLite { Id = 2, Name = "Bob", Age = 23 },
                new StudentLite { Id = 3, Name = "Charlie", Age = 19 },
                new StudentLite { Id = 4, Name = "David", Age = 22 },
                new StudentLite { Id = 5, Name = "Eva", Age = 25 }
            };

            // 1️⃣ Basic IEnumerable Demo
            Console.WriteLine("=== 1️⃣ IEnumerable (In-Memory Collection) ===");

            IEnumerable<StudentLite> enumerableQuery = students
                .Where(s => s.Age > 20)
                .OrderBy(s => s.Name);

            Console.WriteLine("➡️ Deferred execution: query is defined but not executed yet.\n");
            Console.WriteLine("🔍 Enumerating results...");
            foreach (var s in enumerableQuery)
                Console.WriteLine($"{s.Name} ({s.Age})");

            Console.WriteLine("\n🧠 IEnumerable works *in-memory* using LINQ-to-Objects.\n");

            // 2️⃣ Simulated IQueryable Demo
            Console.WriteLine("=== 2️⃣ IQueryable (Simulated LINQ Provider) ===");

            IQueryable<StudentLite> queryableData = students.AsQueryable();

            IQueryable<StudentLite> queryableQuery =
                queryableData.Where(s => s.Age > 20).OrderBy(s => s.Name);

            Console.WriteLine("➡️ Query is built as an expression tree, not executed yet.");
            Console.WriteLine("🔍 Simulating query execution...");
            foreach (var s in queryableQuery)
                Console.WriteLine($"{s.Name} ({s.Age})");

            Console.WriteLine("\n🧠 IQueryable builds *expression trees* — executed by external providers (like SQL).\n");

            // 3️⃣ Deferred vs Immediate Execution
            Console.WriteLine("=== 3️⃣ Deferred vs Immediate Execution ===");

            var deferred = students.Where(s => s.Age > 20); // deferred
            var immediate = students.Where(s => s.Age > 20).ToList(); // immediate

            students.Add(new StudentLite { Id = 6, Name = "Frank", Age = 30 });

            Console.WriteLine("Deferred query result (evaluated after add):");
            foreach (var s in deferred) Console.WriteLine($"  {s.Name}");

            Console.WriteLine("\nImmediate query result (evaluated before add):");
            foreach (var s in immediate) Console.WriteLine($"  {s.Name}");

            Console.WriteLine("\n🧠 Deferred queries re-run when enumerated; immediate ones store results instantly.\n");

            // 4️⃣ Expression Tree Visualization (Advanced)
            Console.WriteLine("=== 4️⃣ Expression Tree Visualization ===");
            Console.WriteLine(queryableQuery.Expression);
            Console.WriteLine("\n🧠 Expression trees are how IQueryable sends instructions to remote providers (like SQL).\n");

            // 5️⃣ Performance Demo (Simulated)
            Console.WriteLine("=== 5️⃣ Performance Impact Simulation ===");

            Console.WriteLine("IEnumerable: All data loaded in memory → filtered locally.");
            Console.WriteLine("IQueryable: Filtering happens on the server before data is sent.");

            Console.WriteLine("\n===== ✅ END OF IEnumerable vs IQueryable DEMO =====");
        }

        // Lightweight class to avoid conflicts with existing Student
        public class StudentLite
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }

            public override string ToString() => $"{Name} ({Age})";
        }
    }
}
