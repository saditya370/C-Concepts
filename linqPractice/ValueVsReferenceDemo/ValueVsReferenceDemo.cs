using System;

namespace linqPractice
{
    public static class ValueVsReferenceDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== ⚖️ VALUE TYPES vs REFERENCE TYPES DEMO =====\n");

            // 1️⃣ Copy Behavior — Value vs Reference
            Console.WriteLine("=== 1️⃣ COPY BEHAVIOR ===");

            int a = 10;
            int b = a;
            b++;
            Console.WriteLine($"Value Types → a={a}, b={b} (Separate copies)");

            MyClass obj1 = new MyClass { Number = 10 };
            MyClass obj2 = obj1;
            obj2.Number++;
            Console.WriteLine($"Reference Types → obj1.Number={obj1.Number}, obj2.Number={obj2.Number} (Same object)\n");

            // 2️⃣ Boxing & Unboxing
            Console.WriteLine("=== 2️⃣ BOXING & UNBOXING ===");

            int num = 42;        // value type
            object boxed = num;  // BOXING → value → heap object
            int unboxed = (int)boxed; // UNBOXING → heap → stack value

            Console.WriteLine($"Boxed: {boxed}, Unboxed: {unboxed}");
            Console.WriteLine("🧠 Boxing copies the value into the heap (slower, avoid in tight loops).\n");

            // 3️⃣ Mutability vs Immutability
            Console.WriteLine("=== 3️⃣ MUTABILITY vs IMMUTABILITY ===");

            MutablePerson m1 = new MutablePerson("Alice");
            m1.Name = "Bob";
            Console.WriteLine($"MutablePerson changed to: {m1.Name}");

            ImmutablePerson im1 = new ImmutablePerson("Charlie");
            // im1.Name = "David"; ❌ Not allowed — immutable
            ImmutablePerson im2 = im1.WithName("David");
            Console.WriteLine($"ImmutablePerson: {im1.Name} → {im2.Name}\n");

            // 4️⃣ Struct vs Class Copy Behavior
            Console.WriteLine("=== 4️⃣ STRUCT vs CLASS COPY ===");

            PointStruct ps1 = new PointStruct(10, 20);
            PointStruct ps2 = ps1; // copy
            ps2.X = 99;

            PointClass pc1 = new PointClass(10, 20);
            PointClass pc2 = pc1; // same reference
            pc2.X = 99;

            Console.WriteLine($"Struct Copy → ps1.X={ps1.X}, ps2.X={ps2.X}");
            Console.WriteLine($"Class Copy → pc1.X={pc1.X}, pc2.X={pc2.X}\n");

            // 5️⃣ Records (Simulated)
            Console.WriteLine("=== 5️⃣ RECORD (Simulated in C# 7.3) ===");

            RecordLikePerson r1 = new RecordLikePerson("Eva", 25);
            RecordLikePerson r2 = r1.Copy("Fiona", 30);

            Console.WriteLine($"Original: {r1}");
            Console.WriteLine($"Copied with new data: {r2}\n");

            Console.WriteLine("===== ✅ END OF VALUE vs REFERENCE DEMO =====");
        }

        // ========== SUPPORT CLASSES AND STRUCTS ========== //

        public class MyClass
        {
            public int Number;
        }

        public class MutablePerson
        {
            public string Name { get; set; }
            public MutablePerson(string name) { Name = name; }
        }

        public class ImmutablePerson
        {
            public string Name { get; }
            public ImmutablePerson(string name) { Name = name; }
            public ImmutablePerson WithName(string newName) => new ImmutablePerson(newName);
        }

        public struct PointStruct
        {
            public int X, Y;
            public PointStruct(int x, int y) { X = x; Y = y; }
        }

        public class PointClass
        {
            public int X, Y;
            public PointClass(int x, int y) { X = x; Y = y; }
        }

        // Simulating record (since records are C# 9+)
        public class RecordLikePerson
        {
            public string Name { get; }
            public int Age { get; }
            public RecordLikePerson(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public RecordLikePerson Copy(string name = null, int? age = null)
            {
                return new RecordLikePerson(name ?? this.Name, age ?? this.Age);
            }

            public override string ToString() => $"{Name}, Age {Age}";
        }
    }
}
