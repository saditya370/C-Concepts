using System;

namespace linqPractice
{
    // ===================== ❓ NULLABLE TYPES DEMO ===================== //
    public static class NullableTypesDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== ❓ NULLABLE TYPES DEMO =====\n");

            // 1️⃣ BASIC: WHAT IS NULLABLE?
            Console.WriteLine("=== 1️⃣ Basic Example ===");
            int? age = null; // normal int cannot be null, but int? can!
            Console.WriteLine("Age: " + (age.HasValue ? age.Value.ToString() : "No age provided"));

            age = 25;
            Console.WriteLine("Updated Age: " + age);

            // 2️⃣ INTERMEDIATE: NULL COALESCING OPERATOR (??)
            Console.WriteLine("\n=== 2️⃣ Using ?? (Null-Coalescing Operator) ===");
            int? marks = null;
            int finalMarks = marks ?? 50; // if marks is null, use 50
            Console.WriteLine("Final Marks: " + finalMarks);

            // 3️⃣ INTERMEDIATE: NULLABLE WITH STRINGS & ??=
            Console.WriteLine("\n=== 3️⃣ Using ??= (Assign If Null) ===");
            string studentName = null;
            if (studentName == null)
            {
                studentName = "Unknown Student";
            }
            Console.WriteLine("Student Name: " + studentName);

            // 4️⃣ ADVANCED: NULL-CONDITIONAL OPERATOR (?.)
            Console.WriteLine("\n=== 4️⃣ Using ?. (Null-Conditional Operator) ===");

            Learner learner = null;
            // Prevents NullReferenceException
            Console.WriteLine("Learner Course: " + (learner != null ? learner.Course : "No course available"));

            learner = new Learner { Name = "Alice", Course = "Mathematics" };
            Console.WriteLine("Learner Course: " + (learner != null ? learner.Course : "No course available"));

            // 5️⃣ ADVANCED COMBO: ?. + ?? + ??=
            Console.WriteLine("\n=== 5️⃣ Combo Example ===");

            Learner unknown = null;
            string course = (unknown != null && unknown.Course != null) ? unknown.Course : "Course not found";
            Console.WriteLine("Course Info: " + course);

            if (unknown == null)
            {
                unknown = new Learner { Name = "Bob", Course = "Physics" };
            }
            Console.WriteLine("New Learner Added: " + unknown.Name + " (" + unknown.Course + ")");

            Console.WriteLine("\n===== ✅ END OF NULLABLE TYPES DEMO =====");
        }

        // ✅ Custom class (avoiding Student conflict)
        private class Learner
        {
            public string Name { get; set; }
            public string Course { get; set; }
        }
    }
}
