using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace linqPractice
{
    /// <summary>
    /// ===========================================
    /// 🧠 ADVANCED COLLECTIONS DEMO
    /// ===========================================
    /// This example demonstrates:
    /// 1️⃣ Custom Sorting using IComparable<T> & IComparer<T>
    /// 2️⃣ ObservableCollection<T> — automatic change tracking
    /// 3️⃣ ConcurrentDictionary — thread-safe dictionary operations
    ///
    /// By the end, you'll understand both single-threaded and
    /// multi-threaded collection management in real-world apps.
    /// </summary>
    public static class CollectionsAdvancedDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== 🧠 ADVANCED COLLECTIONS DEMO =====\n");

            ComparableAndComparerDemo();
            ObservableCollectionDemo();
            ConcurrentDictionaryDemo();

            Console.WriteLine("\n===== ✅ END OF ADVANCED COLLECTIONS DEMO =====");
        }

        // ============================================================
        // 1️⃣ Custom Sorting — IComparable & IComparer
        // ============================================================
        private static void ComparableAndComparerDemo()
        {
            Console.WriteLine("=== 1️⃣ Custom Sorting using IComparable & IComparer ===");

            List<StudentAdv> students = new List<StudentAdv>
            {
                new StudentAdv { Id = 1, Name = "Alice", Marks = 88 },
                new StudentAdv { Id = 2, Name = "Bob", Marks = 75 },
                new StudentAdv { Id = 3, Name = "Charlie", Marks = 92 },
                new StudentAdv { Id = 4, Name = "David", Marks = 60 },
            };

            Console.WriteLine("\n🔹 Original Order:");
            students.ForEach(s => Console.WriteLine(s));

            // Default sort uses IComparable (by Marks)
            students.Sort();
            Console.WriteLine("\n🔹 Sorted by Marks (IComparable):");
            students.ForEach(s => Console.WriteLine(s));

            // Custom comparer (by Name)
            students.Sort(new StudentNameComparer());
            Console.WriteLine("\n🔹 Sorted by Name (IComparer):");
            students.ForEach(s => Console.WriteLine(s));

            Console.WriteLine();
        }

        // ============================================================
        // 2️⃣ ObservableCollection — detects add/remove events
        // ============================================================
        private static void ObservableCollectionDemo()
        {
            Console.WriteLine("=== 2️⃣ ObservableCollection<T> Example ===");

            ObservableCollection<string> courses = new ObservableCollection<string>();

            // Subscribe to collection changed event
            courses.CollectionChanged += (sender, e) =>
            {
                Console.WriteLine($"📣 Collection changed! Action: {e.Action}");
                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems)
                        Console.WriteLine($"✅ Added: {item}");
                }
                if (e.OldItems != null)
                {
                    foreach (var item in e.OldItems)
                        Console.WriteLine($"❌ Removed: {item}");
                }
            };

            // Perform some actions
            courses.Add("Mathematics");
            courses.Add("Physics");
            courses.Add("Computer Science");
            courses.Remove("Physics");
            courses.Add("Chemistry");

            Console.WriteLine("✅ Current Courses:");
            foreach (var course in courses)
                Console.WriteLine($"   {course}");

            Console.WriteLine();
        }

        // ============================================================
        // 3️⃣ ConcurrentDictionary — thread-safe operations
        // ============================================================
        private static void ConcurrentDictionaryDemo()
        {
            Console.WriteLine("=== 3️⃣ ConcurrentDictionary Example (Thread-Safe) ===");

            ConcurrentDictionary<int, string> users = new ConcurrentDictionary<int, string>();

            // Run parallel tasks to simulate concurrent updates
            Parallel.For(1, 6, i =>
            {
                users.TryAdd(i, $"User{i}");
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} added User{i}");
            });

            Console.WriteLine("\n✅ Users in dictionary:");
            foreach (var kv in users)
                Console.WriteLine($"{kv.Key} → {kv.Value}");

            // Try to update safely
            users.TryUpdate(3, "User3_Updated", "User3");
            users.TryRemove(2, out _);

            Console.WriteLine("\n✅ After Updates:");
            foreach (var kv in users)
                Console.WriteLine($"{kv.Key} → {kv.Value}");

            Console.WriteLine();
        }
    }

    // ============================================================
    // Supporting Classes
    // ============================================================

    /// <summary>
    /// Implements IComparable — allows natural sorting by Marks.
    /// </summary>
    public class StudentAdv : IComparable<StudentAdv>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Marks { get; set; }

        public int CompareTo(StudentAdv other)
        {
            if (other == null) return 1;
            return this.Marks.CompareTo(other.Marks); // ascending by Marks
        }

        public override string ToString()
        {
            return $"{Id}. {Name} → Marks: {Marks}";
        }
    }

    /// <summary>
    /// Implements IComparer — provides custom comparison logic (by Name).
    /// </summary>
    public class StudentNameComparer : IComparer<StudentAdv>
    {
        public int Compare(StudentAdv x, StudentAdv y)
        {
            if (x == null || y == null) return 0;
            return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
