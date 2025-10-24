using System;

namespace linqPractice
{
    // ==================== DATA TYPES & MEMORY DEMO ==================== //
    public static class DataTypesAndMemoryDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== ⚙️ DATA TYPES & MEMORY DEMO =====\n");

            // 1️⃣ VALUE TYPES
            Console.WriteLine("=== 1️⃣ VALUE TYPES (Stored on Stack) ===");
            int a = 10;
            int b = a; // value copy
            b = 20;

            Console.WriteLine($"a = {a}, b = {b}");
            Console.WriteLine("✅ Changing 'b' did not affect 'a' because they are separate stack copies.\n");

            // 2️⃣ REFERENCE TYPES
            Console.WriteLine("=== 2️⃣ REFERENCE TYPES (Stored on Heap) ===");
            PersonRef p1 = new PersonRef("Alice");
            PersonRef p2 = p1; // reference copy
            p2.Name = "Bob";

            Console.WriteLine($"p1.Name = {p1.Name}, p2.Name = {p2.Name}");
            Console.WriteLine("⚠️ Changing 'p2' affected 'p1' because both point to the same heap object.\n");

            // 3️⃣ STRUCT VS CLASS
            Console.WriteLine("=== 3️⃣ STRUCT vs CLASS ===");
            PointStruct ps1 = new PointStruct(2, 3);
            PointStruct ps2 = ps1;
            ps2.X = 99;

            PointClass pc1 = new PointClass(2, 3);
            PointClass pc2 = pc1;
            pc2.X = 99;

            Console.WriteLine($"Struct Copy → ps1.X={ps1.X}, ps2.X={ps2.X}");
            Console.WriteLine($"Class Copy → pc1.X={pc1.X}, pc2.X={pc2.X}\n");

            // 4️⃣ PARAMETER PASSING
            Console.WriteLine("=== 4️⃣ PARAMETER PASSING (Value vs Reference) ===");
            int number = 10;
            PersonRef person = new PersonRef("Charlie");

            ChangeValue(number);
            ChangeReference(person);

            Console.WriteLine($"number = {number}");
            Console.WriteLine($"person.Name = {person.Name}\n");

            // 5️⃣ REF & OUT
            Console.WriteLine("=== 5️⃣ REF and OUT Example ===");
            int refVal = 5;
            DoubleIt(ref refVal);
            Console.WriteLine($"After ref: {refVal}");

            int outVal;
            Initialize(out outVal);
            Console.WriteLine($"After out: {outVal}\n");

            Console.WriteLine("===== ✅ END OF DATA TYPES & MEMORY DEMO =====");
        }

        // ========== SUPPORTING METHODS ========== //

        static void ChangeValue(int x)
        {
            x = 999;
            Console.WriteLine($"Inside ChangeValue: x = {x}");
        }

        static void ChangeReference(PersonRef person)
        {
            person.Name = "Updated Inside Method";
            Console.WriteLine($"Inside ChangeReference: Name = {person.Name}");
        }

        static void DoubleIt(ref int n)
        {
            n *= 2;
            Console.WriteLine($"Inside DoubleIt: n = {n}");
        }

        static void Initialize(out int n)
        {
            n = 100;
            Console.WriteLine($"Inside Initialize: n = {n}");
        }

        // ========== CLASSES AND STRUCTS ========== //

        public class PersonRef
        {
            public string Name { get; set; }
            public PersonRef(string name) => Name = name;
        }

        public struct PointStruct
        {
            public int X;
            public int Y;
            public PointStruct(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public class PointClass
        {
            public int X;
            public int Y;
            public PointClass(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
