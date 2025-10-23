using System;
using System.Collections;
using System.Collections.Generic;

namespace linqPractice
{
    public static class CollectionsAndGenericsDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== C# Collections and Generics Demo =====\n");

            // 1. Non-Generic Collections
            Console.WriteLine("=== 1. ArrayList & Hashtable ===");
            ArrayList arr = new ArrayList { 10, "Hello", true };
            foreach (var item in arr)
                Console.WriteLine($" - {item} ({item.GetType().Name})");

            Hashtable ht = new Hashtable
            {
                ["Id"] = 101,
                ["Name"] = "Alice",
                ["Active"] = true
            };
            foreach (DictionaryEntry entry in ht)
                Console.WriteLine($"{entry.Key}: {entry.Value}");

            // 2. Generic Collections
            Console.WriteLine("\n=== 2. Generic Collections ===");
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            Console.WriteLine("List: " + string.Join(", ", numbers));

            Dictionary<int, string> dict = new Dictionary<int, string>
            {
                [1] = "Alice",
                [2] = "Bob"
            };
            foreach (var kv in dict)
                Console.WriteLine($"{kv.Key}: {kv.Value}");

            Stack<string> stack = new Stack<string>();
            stack.Push("A");
            stack.Push("B");
            Console.WriteLine("Stack pop: " + stack.Pop());

            Queue<string> queue = new Queue<string>();
            queue.Enqueue("X");
            queue.Enqueue("Y");
            Console.WriteLine("Queue dequeue: " + queue.Dequeue());

            // 3. HashSet
            Console.WriteLine("\n=== 3. HashSet ===");
            HashSet<int> set = new HashSet<int> { 1, 2, 2, 3 };
            Console.WriteLine("Unique: " + string.Join(", ", set));

            // 4. Custom Generic Class
            Console.WriteLine("\n=== 4. Custom Generic Class ===");
            Box<int> intBox = new Box<int>(123);
            Box<string> strBox = new Box<string>("Hello");
            intBox.Display();
            strBox.Display();

            // 5. Generic Method
            Console.WriteLine("\n=== 5. Generic Method ===");
            int a = 5, b = 10;
            Utilities.Swap(ref a, ref b);
            Console.WriteLine($"Swapped: a={a}, b={b}");

            Console.WriteLine("\n=== END OF COLLECTIONS & GENERICS DEMO ===");
        }
    }

    // Helper classes
    public class Box<T>
    {
        public T Value { get; set; }
        public Box(T value) => Value = value;
        public void Display() => Console.WriteLine($"Box contains: {Value}");
    }

    public static class Utilities
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
