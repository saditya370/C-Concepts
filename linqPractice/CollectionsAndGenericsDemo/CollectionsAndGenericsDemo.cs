using System;
using System.Collections;
using System.Collections.Generic;

namespace linqPractice
{
    /// <summary>
    /// Demonstrates:
    /// 1. Non-generic collections (ArrayList, Hashtable)
    /// 2. Generic collections (List, Dictionary, Stack, Queue, HashSet)
    /// 3. Custom generic class
    /// 4. Generic method
    /// </summary>
    public static class CollectionsAndGenericsDemo
    {
        public static void Run()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("📦 C# Collections & Generics Demo (.NET 4.8)");
            Console.WriteLine("=============================================\n");

            // ==============================================
            // 1️⃣ NON-GENERIC COLLECTIONS (Legacy)
            // ==============================================
            Console.WriteLine("=== 1️⃣ Non-Generic Collections (ArrayList & Hashtable) ===");

            // ArrayList can store mixed types, but not type-safe.
            ArrayList arr = new ArrayList { 10, "Hello", true };
            foreach (var item in arr)
                Console.WriteLine(" - " + item + " (" + item.GetType().Name + ")");

            // Hashtable — stores key-value pairs, but uses object types internally.
            Hashtable ht = new Hashtable();
            ht["Id"] = 101;
            ht["Name"] = "Alice";
            ht["Active"] = true;

            foreach (DictionaryEntry entry in ht)
                Console.WriteLine("   " + entry.Key + ": " + entry.Value);

            Console.WriteLine("\n⚠️  Non-generic collections are flexible but unsafe.");
            Console.WriteLine("   - No compile-time type safety");
            Console.WriteLine("   - Requires casting / boxing / unboxing");
            Console.WriteLine("   - Slower and prone to runtime errors\n");

            // ==============================================
            // 2️⃣ GENERIC COLLECTIONS (Type-Safe)
            // ==============================================
            Console.WriteLine("=== 2️⃣ Generic Collections ===");

            // List<T> — dynamic, type-safe, ordered
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            numbers.Add(6);
            Console.WriteLine("List<int>: " + string.Join(", ", numbers.ToArray()));

            // Dictionary<TKey, TValue> — fast lookups with key-value pairs
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict[1] = "Alice";
            dict[2] = "Bob";
            dict[3] = "Charlie";

            Console.WriteLine("Dictionary<int, string>:");
            foreach (var kv in dict)
                Console.WriteLine("   " + kv.Key + ": " + kv.Value);

            // Stack<T> — LIFO (Last-In-First-Out)
            Stack<string> stack = new Stack<string>();
            stack.Push("A");
            stack.Push("B");
            Console.WriteLine("Stack Pop → " + stack.Pop());

            // Queue<T> — FIFO (First-In-First-Out)
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("X");
            queue.Enqueue("Y");
            Console.WriteLine("Queue Dequeue → " + queue.Dequeue());

            // HashSet<T> — stores only unique elements
            HashSet<int> set = new HashSet<int> { 1, 2, 2, 3 };
            Console.WriteLine("HashSet<int> (unique): " + string.Join(", ", set));

            Console.WriteLine("\n✅ Generics give:");
            Console.WriteLine("   - Compile-time type safety");
            Console.WriteLine("   - No boxing/unboxing");
            Console.WriteLine("   - Better performance\n");

            // ==============================================
            // 3️⃣ CUSTOM GENERIC CLASS
            // ==============================================
            Console.WriteLine("=== 3️⃣ Custom Generic Class ===");

            Box<int> intBox = new Box<int>(123);
            Box<string> strBox = new Box<string>("Hello World");
            intBox.Display();
            strBox.Display();

            // Demonstrating with DateTime
            Box<DateTime> dateBox = new Box<DateTime>(DateTime.Now);
            dateBox.Display();

            Console.WriteLine("\n💡 Custom generic classes promote code reuse and type safety.\n");

            // ==============================================
            // 4️⃣ GENERIC METHOD
            // ==============================================
            Console.WriteLine("=== 4️⃣ Generic Method ===");

            int a = 5, b = 10;
            Utilities.Swap<int>(ref a, ref b);
            Console.WriteLine("Swapped ints → a=" + a + ", b=" + b);

            string s1 = "foo", s2 = "bar";
            Utilities.Swap<string>(ref s1, ref s2);
            Console.WriteLine("Swapped strings → s1=" + s1 + ", s2=" + s2);

            Console.WriteLine("\n✅ Generic methods allow type-agnostic algorithms.\n");

            Console.WriteLine("=== END OF COLLECTIONS & GENERICS DEMO ===");
        }
    }

    // ==============================================
    // 🧱 Custom Generic Class
    // ==============================================
    public class Box<T>
    {
        public T Value { get; set; }

        public Box(T value)
        {
            Value = value;
        }

        public void Display()
        {
            Console.WriteLine("Box<" + typeof(T).Name + "> contains: " + Value);
        }
    }

    // ==============================================
    // 🧩 Generic Method Utility
    // ==============================================
    public static class Utilities
    {
        /// <summary>
        /// Swaps two variables of any type using generics.
        /// </summary>
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
