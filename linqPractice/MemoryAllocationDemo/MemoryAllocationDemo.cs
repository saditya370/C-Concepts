using System;
using System.Collections.Generic;

namespace linqPractice
{
    public static class MemoryAllocationDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== ⚙️ MEMORY ALLOCATION DEMO (Stack vs Heap) =====\n");

            // 1️⃣ VALUE TYPE — Stored on STACK
            Console.WriteLine("=== 1️⃣ Value Types Example ===");
            int a = 10;
            int b = a; // copies value
            b = 20;

            Console.WriteLine($"a = {a}, b = {b}");
            Console.WriteLine("🧠 Each variable gets its own copy (stored on the stack).");
            Console.WriteLine("Changing 'b' does not affect 'a'.\n");

            // 2️⃣ REFERENCE TYPE — Stored on HEAP
            Console.WriteLine("=== 2️⃣ Reference Types Example ===");
            PersonRef p1 = new PersonRef { Name = "Alice", Age = 25 };
            PersonRef p2 = p1; // copies reference
            p2.Age = 30;

            Console.WriteLine($"p1.Age = {p1.Age}, p2.Age = {p2.Age}");
            Console.WriteLine("🧠 Both variables point to the SAME object on the heap.\n");

            // 3️⃣ STRUCT (Value Type)
            Console.WriteLine("=== 3️⃣ Struct Example ===");
            PointStruct s1 = new PointStruct(3, 5);
            PointStruct s2 = s1; // copy value
            s2.X = 10;

            Console.WriteLine($"s1 = ({s1.X}, {s1.Y}), s2 = ({s2.X}, {s2.Y})");
            Console.WriteLine("🧠 Structs are copied by value (stack allocated). Changing one does not affect the other.\n");

            // 4️⃣ CLASS (Reference Type)
            Console.WriteLine("=== 4️⃣ Class Example ===");
            PointClass c1 = new PointClass(3, 5);
            PointClass c2 = c1; // reference copy
            c2.X = 10;

            Console.WriteLine($"c1 = ({c1.X}, {c1.Y}), c2 = ({c2.X}, {c2.Y})");
            Console.WriteLine("🧠 Classes are reference types (heap allocated). Both refer to same memory.\n");

            // 5️⃣ STACK vs HEAP Visualization
            Console.WriteLine("=== 5️⃣ Stack vs Heap Visualization ===");
            Console.WriteLine(@"
   Stack (short-lived, value types)
   ┌───────────────┐
   │ int a = 10    │
   │ int b = 20    │
   │ s1 (copy)     │
   └───────────────┘
          ↓
   Heap (long-lived, reference types)
   ┌───────────────────────────┐
   │ PersonRef {Name=Alice,Age=30} │
   │ PointClass {X=10, Y=5}    │
   └───────────────────────────┘
");
            Console.WriteLine("🧠 Stack stores variables directly. Heap stores objects — stack holds *pointers* to them.\n");

            // 6️⃣ IMMUTABLE vs MUTABLE EXAMPLE
            Console.WriteLine("=== 6️⃣ Immutable vs Mutable ===");
            string s = "Hello";
            string sCopy = s;
            sCopy += " World";

            Console.WriteLine($"s = {s}, sCopy = {sCopy}");
            Console.WriteLine("🧠 Strings are immutable: changing one creates a *new* object on the heap.\n");

            // 7️⃣ Boxing & Unboxing
            Console.WriteLine("=== 7️⃣ Boxing & Unboxing ===");
            int num = 123;
            object boxed = num;         // Boxing (value -> object)
            int unboxed = (int)boxed;   // Unboxing (object -> value)

            Console.WriteLine($"num = {num}, boxed = {boxed}, unboxed = {unboxed}");
            Console.WriteLine("🧠 Boxing stores a *copy* of the value type in the heap as an object.\n");

            // 8️⃣ Garbage Collection
            Console.WriteLine("=== 8️⃣ Garbage Collection Concept ===");
            Console.WriteLine("🧹 Objects on the heap are automatically cleaned up by the Garbage Collector (GC).");
            Console.WriteLine("When no variable references an object, it's eligible for collection.\n");

            Console.WriteLine("===== ✅ END OF MEMORY ALLOCATION DEMO =====");
        }

        // Reference Type
        public class PersonRef
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        // Struct (Value Type)
        public struct PointStruct
        {
            public int X { get; set; }
            public int Y { get; set; }
            public PointStruct(int x, int y)
            {
                X = x; Y = y;
            }
        }

        // Class (Reference Type)
        public class PointClass
        {
            public int X { get; set; }
            public int Y { get; set; }
            public PointClass(int x, int y)
            {
                X = x; Y = y;
            }
        }
    }
}
